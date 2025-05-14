using System;
using System.Linq;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.QuestSystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.RotatingStatues
{
    public class StatuesObserver:MonoBehaviour
    {
        [SerializeField] private RotatingStatuesManipulator[] _statues;
        [SerializeField] private Door _door;
        [SerializeField] private SaveGameQuestAction _saveGameAction;

        private void Start()
        {
            foreach (var statue in _statues)
            {
                statue.OnStatueRotated += OnStatuesRotated;
            }
        }

        private void OnStatuesRotated()
        {
            Debug.Log("Rotating statues");
            if (_statues.All(s => s.Fixed))
            {
                Debug.Log("Rotating statues all");
                OnAllBedsCompleted();
            }
        }

        private void OnAllBedsCompleted()
        {
            if (!_door.IsOpen)
            {
                _door.Open();
                _saveGameAction.Activate();
            }
        }

        private void OnDestroy()
        {
            foreach (var statue in _statues)
            {
                statue.OnStatueRotated -= OnStatuesRotated;
            }
        }
    }
}