using System;
using System.Linq;
using _Project.Runtime.Core.Interactables;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.RotatingStatues
{
    public class StatuesObserver:MonoBehaviour
    {
        [SerializeField] private RotatingStatuesManipulator[] _statues;
        [SerializeField] private Door _door;

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
            _door.Open();
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