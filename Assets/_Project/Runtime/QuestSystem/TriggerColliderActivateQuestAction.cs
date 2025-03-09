using System;
using _Project.Runtime.Core.Herbalist;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class TriggerColliderActivateQuestAction : MonoBehaviour
    {
        [SerializeField] private BaseQuestAction[] _questActions;
        [SerializeField] private bool _singleUse = true;

        private int _useCount = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (_singleUse && _useCount > 0)
            {
                return;
            }

            if (other.TryGetComponent<Herbalist>(out var herbalist))
            {
                _useCount++;
                foreach (var quest in _questActions)
                {
                    quest.Activate();
                }
            }
        }
    }
}