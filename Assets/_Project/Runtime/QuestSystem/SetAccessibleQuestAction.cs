using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class SetAccessibleQuestAction : BaseQuestAction
    {
        [SerializeField] private Interactable[] _interactables;

        [SerializeField] private bool _makeAccessible;
        public override void Activate()
        {
            foreach (var interactable in _interactables)
            {
                interactable.IsAccessible = _makeAccessible;
            }
        }
    }
}