using _Project.Runtime.Core.Interactables;
using _Project.Runtime.QuestSystem;
using UnityEngine;

namespace _Project.Runtime.Core.Enemies
{
    public class DragonAss :Creature
    {
        [SerializeField]
        private DialogueInteractionTrigger _dialogue;
        [SerializeField]
        private BaseQuestAction _onHit;
        
        
        private void Start()
        {
            this.Hit += () =>
            {
                Debug.Log(name + " hit");
                _dialogue.Interact();
                _onHit?.Activate();
            };
        }
    }
}