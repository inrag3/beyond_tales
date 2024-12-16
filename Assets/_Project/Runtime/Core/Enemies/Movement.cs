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

        [Inject]
        private void Construct(NavMeshAgent agent, EnemyAnimer animer)
        {
            _animer = animer;
            _agent = agent;
        }
        private void Update() => 
            _currentCooldown = Max(_currentCooldown - deltaTime, 0f);

        public void Move(Vector3 at)
        {
            _animer.PlayMove(_agent.velocity.magnitude);
            if (_agent.destination == at || InCooldown)
                return;
            
            Resume();
            _agent.SetDestination(at);
            _currentCooldown = _updatePathCooldown;
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