using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class OpenDoorQuestAction : BaseQuestAction
    {
        [SerializeField] private Door _door;
        [SerializeField] private bool _open;
        
        public override void Activate()
        {
            if (_open)
            {
                _door.Open();
            }
            else
            {
                _door.Close();
            }
        }
    }
}