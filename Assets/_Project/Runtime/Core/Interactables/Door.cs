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
        [SerializeField] private bool _singleUse;
        [SerializeField] private bool _isLocked;
        [field: SerializeField] public bool IsOpen { get; private set; } = false;

        private bool _isAnimationPlaying = false;
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
        }

        public void SwitchState()
        {
            if (_isLocked)
            {
                return;
            }

            IsAccessible = false;
            if (IsOpen)
            {
               Close();
            }
            else
            {
                Open();
            }
        }

        public void Close()
        {
            _playableDirector.Play(_closeDoorAnimation);
            _isAnimationPlaying = true;
            _navMeshObstacle.carving = true;
            IsOpen = false;
        }

        public void Open()
        {
            _playableDirector.Play(_openDoorAnimation);
            _isAnimationPlaying = true;
            IsOpen = true;
        }

        public void Lock(bool isLock)
        {
            _isLocked = isLock;
            if (_isLocked)
            {
                IsAccessible = false;
            }
            else
            {
                if (!_isAnimationPlaying)
                {
                    IsAccessible = true;
                }
            }
        }

        private void OnStopPlayAnimation(PlayableDirector director)
        {
            _isAnimationPlaying = false;
            if (!_singleUse && !_isLocked)
            {
                IsAccessible = true;
            }

            if (IsOpen)
            {
                _navMeshObstacle.carving = false;
            }
        }

    }
}