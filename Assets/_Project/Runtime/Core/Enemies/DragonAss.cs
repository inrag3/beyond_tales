using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.QuestSystem;
using UnityEngine;

namespace _Project.Runtime.Core.Enemies
{
    public class DragonAss : Creature
    {
        [SerializeField] private DialogueInteractionTrigger _dialogue;
        [SerializeField] private List<BaseQuestAction> _onHit;

        private int counter = 0;

        private void Start()
        {
            this.Hit += () =>
            {
                Debug.Log(name + " hit");
                _dialogue.Interact();
                _onHit[counter]?.Activate();
                counter = Math.Max(counter + 1, _onHit.Count - 1);
            };
        }
    }
}