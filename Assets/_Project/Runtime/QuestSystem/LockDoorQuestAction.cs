using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class LockDoorQuestAction : BaseQuestAction
    {
        [SerializeField] private Door _door;
        [SerializeField] private bool _lock;
        
        public override void Activate()
        {
            _door.Lock(_lock);
        }
    }
}