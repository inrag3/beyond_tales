using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class ActivateComponentsQuestAction : BaseQuestAction
    {
        [SerializeField] private MonoBehaviour[] _components;
        [SerializeField] private bool _makeActive;
        
        public override void Activate()
        {
            foreach (var component in _components)
            {
                component.enabled = _makeActive;
            }
        }
    }
}