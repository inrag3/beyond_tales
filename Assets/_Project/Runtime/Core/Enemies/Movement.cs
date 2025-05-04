using System;
using Extensions;
using R3;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Mathf;
using static UnityEngine.Time;
using Zenject;

namespace _Project.Runtime.Core.Enemies
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _updatePathCooldown;
        [SerializeField] private float _speed;
        [SerializeField] private float _currentCooldown;
        private NavMeshAgent _agent;
        private EnemyAnimer _animer;
        private bool InCooldown => _currentCooldown > 0.1f;

        private NavMeshPath _cashPath;

        private bool _ignoreMovementAnimation;

        public float AngularSpeed => _agent.angularSpeed;
        
        [Inject]
        private void Construct(NavMeshAgent agent, EnemyAnimer animer)
        {
            _animer = animer;
            _agent = agent;
        }

        private void Start()
        {
            _cashPath = new NavMeshPath();
        }

        private void Update()
        {
            if (!_ignoreMovementAnimation)
            {
                _animer.PlayMove(_agent.velocity.magnitude);
            }
            _currentCooldown = Max(_currentCooldown - deltaTime, 0f);
        }

        public void Move(Vector3 at)
        {
            if (_agent.destination == at || InCooldown)
                return;
            
            Resume();
            _agent.SetDestination(at);
            _currentCooldown = _updatePathCooldown;
        }


        public void Move(Vector3 at, float speed)
        {
            if (_agent.destination == at || InCooldown)
                return;
            
            Resume();
            _agent.speed = speed;
            _agent.SetDestination(at);
            _currentCooldown = _updatePathCooldown;
        }

        public void StopUpdatePosition()
        {
            _agent.updatePosition = false;
        }
        
        public void ResumeUpdatePosition()
        {
            _agent.updatePosition = true;
            _agent.nextPosition = _agent.transform.position;
        }

        public void StopUpdateRotation()
        {
            _agent.updateRotation = false;
        }

        public void ResumeUpdateRotation()
        {
            _agent.updateRotation = true;
        }

        public void IgnoreMovementAnimation(bool ignore)
        {
            _ignoreMovementAnimation = ignore;
        }

        public bool CanAchievePos(Vector3 position)
        {
            if (position.OnNavMesh(out var navMeshPos))
            {
                _agent.CalculatePath(navMeshPos, _cashPath);
                return _cashPath.status == NavMeshPathStatus.PathComplete;
            }
            return false;
        }

        public void Pause()
        {
            _agent.speed = 0f;
        }

        public void Resume()
        {
            _agent.speed = _speed;
        }
    }
}