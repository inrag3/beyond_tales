using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.DialogueSystem;
using _Project.Runtime.Infrastructure.Factories;
using DialogueSystem.Nodes;
using DialogueSystem.Nodes.Checks;
using ElectricServiceCompany;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;


namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {

        /// <summary>
        /// Синглтон паттерн
        /// </summary>
        //public static DialogueManager instance;

        [SerializeField] private RectTransform dialogueView;

        [Tooltip("Ссылка на поле, куда выводятся все произносимые фразы")] [SerializeField]
        private Text messageText;
        [Tooltip("Ссылка на текстовое поле имени правого собеседника")]
        [SerializeField]
        protected Text rightActorName;
        [Tooltip("Ссылка на аватарку правого собеседника")]
        [SerializeField]
        protected Image rightActorAvatar;
        
        [Tooltip("Ссылка на текстовое поле имени левого собеседника")]
        [SerializeField]
        protected Text leftActorName;
        [Tooltip("Ссылка на аватарку левого собеседника")]
        [SerializeField]
        protected Image leftActorAvatar;

        [Tooltip("Ссылка на панель ответов")] 
        [SerializeField]
        protected AnswersPanel answersPanel;
        
        [Tooltip("Актер игрока. Всегда неявно инициализируется в диалоге")]
        [SerializeField]
        protected Actor playerActor;

        [Header("TypeWriter settings")] [Tooltip("Скорость распечатки сообщений")] [SerializeField]
        private float typeWriterSpeed = 20f;

        [Tooltip("Ожидание в секундах перед началом печати")] [SerializeField]
        protected float startDelay = 0.5f;

        [Tooltip("Задержка между завершением печати и возможностью перейти к следующему сообщению")] [SerializeField]
        protected float preEndDelay = 0.5f;

        [Tooltip("Задержка после окончания написания сообщения")] [SerializeField]
        protected float endDelay = 1f;

        /// <summary>
        /// Нода, обрабатываемая в данный момент
        /// </summary>
        protected Node currentNode;

        /// <summary>
        /// Нода, которую надо обработать следующей
        /// </summary>
        protected Node nextNode;
        
        /// <summary>
        /// Список актеров в текущем диалоге
        /// </summary>
        protected List<Actor> actors;
        /// <summary>
        /// Варианты ответов, закэшированные через CashAnswerNode
        /// </summary>
        protected List<(Node, Answer)> cashedAnswers;

        protected CanvasGroup dialogueHider;

        protected bool isDialogueOpen;

        public bool IsDialogueOpen => isDialogueOpen;
        public event Action Ended;

        private IPlayerInventory _playerInventory;

        private IFlowerFactory _flowerFactory;

        private IHerbalistProvider _herbalistProvider;
        
        [Inject]
        public void Construct(IPlayerInventory playerInventory, IFlowerFactory flowerFactory, IHerbalistProvider herbalistProvider)
        {
            dialogueHider = GetComponent<CanvasGroup>();
            Debug.Log(dialogueHider.IsNullOrDestroyed());
            cashedAnswers=new List<(Node, Answer)>();
            HideDialogueView();
            actors= new List<Actor>();
            actors.Add(playerActor);

            _playerInventory = playerInventory;
            _flowerFactory = flowerFactory;
            _herbalistProvider = herbalistProvider;
        }

        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Инициирует диалог
        /// </summary>
        /// <param name="dialogue"></param>
        public void StartDialogue(DialogueGraph dialogue)
        {
            if (!isDialogueOpen)
            {
                ShowDialogueView();


                StartCoroutine(ProcessTheDialogue(dialogue));
            }
        }

        protected IEnumerator ProcessTheDialogue(DialogueGraph dialogue)
        {
            currentNode = dialogue.GetFirstNode();
            while (currentNode != null)
            {
                yield return StartCoroutine(ProcessCurrentNode());
                currentNode = nextNode;
            }

            HideDialogueView();
        }

        protected IEnumerator ProcessCurrentNode()
        {
            switch (currentNode)
            {
                case MessageNode msg:
                    yield return StartCoroutine(ProcessMessageNode(msg));
                    break;
                case InitializeActorsNode node:
                    yield return StartCoroutine(ProcessInitializeActorsNode(node));
                    break;
                case ChoiceNode node:
                    yield return StartCoroutine(ProcessChoiceNode(node));
                    break;
                case StoryMarksNode node:
                    yield return StartCoroutine(ProcessStoryMarksNode(node));
                    break;
                case CheckNode node:
                    yield return StartCoroutine(ProcessCheckNode(node));
                    break;
                case CashAnswerNode node:
                    yield return StartCoroutine(ProcessCashAnswerNode(node));
                    break;
                case RemoveItemsNode node:
                    yield return StartCoroutine(ProcessRemoveItemsNode(node));
                    break;
                case PlantFlowerForBedNode node:
                    yield return StartCoroutine(ProcessPlantFlowerForBedNode(node));
                    break;
                case SetAccessibleNode node:
                    yield return StartCoroutine(ProcessSetAccessibleNode(node));
                    break;
                case ActivateQuestActionsNode node:
                    yield return StartCoroutine(ProcessActivateQuestActionsNode(node));
                    break;
            }
        }

        protected IEnumerator ProcessMessageNode(MessageNode node)
        {
            nextNode = node.GetNextNode();
            var actor = GetActorByName(node.ActorName);
            if(actor==null) Debug.LogError($"Актера с именем *{node.ActorName}* не существует");

            string actorName = actor.name;
            leftActorName.text = actorName;
            leftActorAvatar.sprite = actor.avatar;
            
            
            dialogueHider.alpha = 1;
            
            yield return StartCoroutine(TypeMessage(node.message));

            yield return new WaitForSeconds(preEndDelay);

            float timer = 0f;
            while (!(Input.GetAxis("Submit") > 0) && timer < endDelay)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        protected IEnumerator ProcessInitializeActorsNode(InitializeActorsNode node)
        {
            //Debug.Log(node.initializableActors.Length);
            for (int i = 0; i < node.initializableActors.Length; i++)
            {
                actors.Add(node.initializableActors[i]);
                //Debug.Log($"{node.initializableActors[i].name} added");
            }

            nextNode = node.GetNextNode();
            yield break;
        }

        protected IEnumerator ProcessChoiceNode(ChoiceNode node)
        {
            List<Answer> tempAnswerList = new List<Answer>(node.answers);
            tempAnswerList.AddRange(cashedAnswers.ConvertAll(t=>t.Item2));
            answersPanel.SetAnswers(tempAnswerList);
            yield return new WaitWhile(()=>answersPanel.IsActive);
            int choice = answersPanel.SelectionPointer;
            //Debug.Log($"Choice = {choice}");
            if (choice >= node.answers.Count)
            {
                nextNode = cashedAnswers[choice - node.answers.Count].Item1;
            }
            else
            {
                nextNode=node.GetNodeByAnswer(choice);   
            }
            cashedAnswers.Clear();
        }

        protected IEnumerator ProcessCashAnswerNode(CashAnswerNode node)
        {
            cashedAnswers.Add((node.GetAnswerNode(),node.answer));
            nextNode = node.GetNextNode();
            yield break;
        }

        protected IEnumerator ProcessStoryMarksNode(StoryMarksNode node)
        {
            _herbalistProvider.Herbalist.PlayerData.AddStoryMarks(node.MarksToAdd);
            _herbalistProvider.Herbalist.PlayerData.RemoveStoryMarks(node.MarksToRemove);
            nextNode = node.GetNextNode();
            yield break;
        }
        
        protected IEnumerator ProcessRemoveItemsNode(RemoveItemsNode node)
        {
            _playerInventory.RemoveItems(node.itemsToRemove);
            nextNode = node.GetNextNode();
            yield break;
        }

        protected IEnumerator ProcessPlantFlowerForBedNode(PlantFlowerForBedNode node)
        {
            var flower = _flowerFactory.Create(node.FlowerType);
            node.Bed.Plant(flower);
            nextNode = node.GetNextNode();
            yield break;
        }

        protected IEnumerator ProcessSetAccessibleNode(SetAccessibleNode node)
        {
            node.Interactable.IsAccessible = node.IsAccessible;
            nextNode = node.GetNextNode();
            yield break;
        }

        protected IEnumerator ProcessCheckNode(CheckNode node)
        {
            bool flag = true;
            
            //Todo Решить нужны ли нам storyMarks и если да то, добавить для них где-то хранилище
            switch (node)
            {
                case StoryMarksCheckNode marksNode:
                    flag = !_herbalistProvider.Herbalist.PlayerData.HasAtLeastOneMark(marksNode.ForbiddenMarks) &&
                           _herbalistProvider.Herbalist.PlayerData.HasStoryMarks(marksNode.RequiredMarks);
                    break;
                case InventoryCheckNode checkNode:
                    flag = _playerInventory.HasItems(checkNode.requiredItems);
                    break;
                case AddItemsNode addNode:
                    flag = _playerInventory.CanAddItems(addNode.itemsToAdd,
                        addNode.AddIfPossible);
                    break;
                case HealthCheck healthCheckNode:
                    flag = _herbalistProvider.Herbalist.Health.Value.CurrentValue < healthCheckNode.Threshold;
                    if (!healthCheckNode.NeedLower)
                    {
                        flag = !flag;
                    }
                    break;
            }

            nextNode = node.GetNextNodeByCheck(flag);
            yield break;
        }

        protected IEnumerator ProcessActivateQuestActionsNode(ActivateQuestActionsNode node)
        {
            foreach (var questAction in node.QuestActions)
            {
                questAction.Activate();
            }
            
            nextNode = node.GetNextNode();
            yield break;
        }


        protected IEnumerator TypeMessage(string message)
        {
            messageText.text = "";
            yield return new WaitForSeconds(startDelay);
            float t = 0;
            int charIndex = 0;

            while (charIndex < message.Length && !(Input.GetAxis("Submit") > 0))
            {
                t += Time.deltaTime * typeWriterSpeed;

                charIndex = Mathf.FloorToInt(t);

                messageText.text = message.Substring(0, charIndex);

                yield return null;
            }

            messageText.text = message;

        }

        protected void ShowDialogueView()
        {
            dialogueView.gameObject.SetActive(true);
            //dialogueHider.alpha = 1;
            isDialogueOpen = true;
            //Todo - обрабатывать начало диалога здесь
            /*Instance.InputDetectionActive = false;
            DialogueEvent.Trigger(DialogueEventType.DialogueOpen);*/
        }

        protected void HideDialogueView()
        {
            Ended?.Invoke();
            dialogueHider.alpha = 0;
            isDialogueOpen = false;
            cashedAnswers.Clear();
            dialogueView.gameObject.SetActive(false);
            //Todo обрабатывать окончание диалога здесь
            /*
            InputManager.Instance.InputDetectionActive = true;
            DialogueEvent.Trigger(DialogueEventType.DialogueClose);*/
        }

        protected Actor GetActorByName(string name)
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i].name == name)
                {
                    return actors[i];
                }
            }

            return null;
        }
        
    }
}
