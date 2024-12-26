using System;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

namespace _Project.Runtime.Core.Interactables
{
    public class Door : Interactable
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private TimelineAsset _openDoorAnimation;
        [SerializeField] private TimelineAsset _closeDoorAnimation;
        [SerializeField] private NavMeshObstacle _navMeshObstacle;
        [field: SerializeField] public bool IsOpen { get; private set; } = false;

        private void OnEnable()
        {
            _playableDirector.stopped += OnStopPlayAnimation;
        }

        private void OnDisable()
        {
            _playableDirector.stopped -= OnStopPlayAnimation;
        }

        public override void Interact(IInteractableVisitor visitor)
        {
            if (!IsAccessible)
            {
                return;
            }

            visitor.Accept(this);
            SwitchState();
        }

        public void SwitchState()
        {
            IsAccessible = false;
            if (IsOpen)
            {
                _playableDirector.Play(_closeDoorAnimation);
                _navMeshObstacle.carving = true;
            }
            else
            {
                _playableDirector.Play(_openDoorAnimation);
            }

            IsOpen = !IsOpen;
        }

        private void OnStopPlayAnimation(PlayableDirector director)
        {
            IsAccessible = true;
            if (IsOpen)
            {
                _navMeshObstacle.carving = false;
            }
        }

    }
}