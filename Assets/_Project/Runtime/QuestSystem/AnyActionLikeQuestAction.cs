using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Runtime.QuestSystem
{
    public class AnyActionLikeQuestAction: BaseQuestAction
    {
        [SerializeField] public UnityEvent _action;
        
        public override void Activate()
        {
            _action?.Invoke();
        }
    }
}