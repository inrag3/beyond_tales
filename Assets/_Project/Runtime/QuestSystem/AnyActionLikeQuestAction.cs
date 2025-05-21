using System;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class AnyActionLikeQuestAction: BaseQuestAction
    {
        [SerializeField] public event Action _action;
        
        public override void Activate()
        {
            _action?.Invoke();
        }
    }
}