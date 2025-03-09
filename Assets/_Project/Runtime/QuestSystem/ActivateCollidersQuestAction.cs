using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Runtime.QuestSystem
{
    public class ActivateCollidersQuestAction : BaseQuestAction
    {
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private bool _makeActive;
        
        public override void Activate()
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = _makeActive;
            }
        }
    }
}