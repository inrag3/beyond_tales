using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class AnswersPanel : MonoBehaviour
{
    [SerializeField]
    protected RectTransform contentTransform;
    
    protected VerticalLayoutGroup contentLayout;

    [SerializeField] protected GameObject answerItemPrefab;

    [SerializeField] protected ScrollRect scrollRect;

    protected CanvasGroup groupHider;

    protected List<AnswerItem> answerItems;
    
    /// <summary>
    /// Показывает активна ли панель в данный момент
    /// </summary>
    protected bool isActive=false;
    /// <summary>
    /// Можно ли сделать выбор в данный момент
    /// </summary>
    protected bool choiceAllowed=false;
    /// <summary>
    /// Номер выбранного в данный момент варианта ответа
    /// </summary>
    protected int selectionPointer;

    public int SelectionPointer => selectionPointer;

    protected RectTransform panelRect;
    private void Awake()
    {
        groupHider = GetComponent<CanvasGroup>();
        contentLayout = contentTransform.GetComponent<VerticalLayoutGroup>();
        panelRect = GetComponent<RectTransform>();
    }

    void Start()
    {
        Initialization();
        HidePanel();
    }
    /// <summary>
    /// Ищем все элементы ответов и записываем в список
    /// </summary>
    protected void Initialization()
    {
        answerItems=new List<AnswerItem>();
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            var cur=contentTransform.GetChild(i);
            var curItem = cur.GetComponent<AnswerItem>();
            if (curItem != null)
            {
                answerItems.Add(curItem);
            }
            curItem.InitializeData(this,i);
        }
    }
    /// <summary>
    /// Добавляет новый элемент ответа, если их меньше чем надо
    /// </summary>
    protected void AddNewAnswerItem()
    {
        var newItem = Instantiate(answerItemPrefab, contentTransform).GetComponent<AnswerItem>();
        newItem.InitializeData(this,answerItems.Count);
        answerItems.Add(newItem);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                UpdateSelection(selectionPointer + 1);
                //scrollRect.normalizedPosition=new Vector2(0,answerItems.Count/(selectionPointer+1));
                //scrollRect.content.anchoredPosition=new Vector2(0,52*selectionPointer);
                UpdateScrollRect();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpdateSelection(selectionPointer - 1);
                UpdateScrollRect();
            }
            else if (Input.GetAxis("Submit") > 0)
            {
                SelectAnswer(selectionPointer);
            }
        }
    }
    /// <summary>
    /// Обновляет положения скролла при выборе через клавиатуре
    /// </summary>
    protected void UpdateScrollRect()
    {
        float sumHeight = contentLayout.padding.top;
        for (int i = 0; i <= selectionPointer-1; i++)
        {
            sumHeight += answerItems[i].GetComponent<RectTransform>().sizeDelta.y+
                         contentLayout.spacing;
        }

        var itemTopEdge = sumHeight;
        var itemDownEdge = sumHeight + answerItems[selectionPointer].GetComponent<RectTransform>().sizeDelta.y;

        var topEdge = contentTransform.anchoredPosition.y;
        var downEdge = contentTransform.anchoredPosition.y + panelRect.sizeDelta.y;
        
        
        if (downEdge<itemDownEdge)
        {
            contentTransform.anchoredPosition =
                contentTransform.anchoredPosition.MMSetY(itemDownEdge - panelRect.sizeDelta.y);
        }
        else if(topEdge>itemTopEdge)
        {
            contentTransform.anchoredPosition=
                contentTransform.anchoredPosition.MMSetY(itemTopEdge);
        }
        
    }

    /// <summary>
    /// Вызывается, когда игрок мышкой наводит на один из вариантов ответов
    /// </summary>
    public void OnHowerItem(int answerNum)
    {
        if (isActive)
        {
            UpdateSelection(answerNum);
        }
    }
    /// <summary>
    /// ВызываетсяЮ когда игрок мышкой выбрал какой-то вариант ответа
    /// </summary>
    public void OnSelectItem(int answerNum)
    {
        if (isActive)
        {
            SelectAnswer(answerNum);
        }
    }

    protected void UpdateSelection(int newNum)
    {
        int rNum = newNum;
        rNum = Mathf.Clamp(rNum, 0, answerItems.Count(a=>a.gameObject.activeSelf)-1);
        answerItems[selectionPointer].SetSelected(false);
        answerItems[rNum].SetSelected(true);
        selectionPointer = rNum;
    }
    /// <summary>
    /// Окончательный выбор ответа
    /// </summary>
    protected void SelectAnswer(int answerNum)
    {
        if (choiceAllowed)
        {
            //Debug.Log(answerNum);
            selectionPointer = answerNum;
            isActive = false;
            HidePanel();
        }
    }

    public void SetAnswers(List<Answer> answers)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            if (i >= answerItems.Count)
            {
                AddNewAnswerItem();
            }
            
            answerItems[i].gameObject.SetActive(true);
            answerItems[i].SetAnswer(answers[i]);
        }

        isActive = true;
        Invoke("AllowChoice",0.5f);
        ShowPanel();
    }

    public bool IsActive => isActive;

    public void ShowPanel()
    {
        groupHider.alpha = 1;
        selectionPointer = 0;
        UpdateSelection(selectionPointer);
    }

    protected void AllowChoice()
    {
        choiceAllowed = true;
    }

    public void HidePanel()
    {
        groupHider.alpha = 0;
        for (int i = 0; i < answerItems.Count; i++)
        {
            answerItems[i].SetSelected(false);
            answerItems[i].gameObject.SetActive(false);
        }
        isActive = false;
        choiceAllowed = false;
    }
}
