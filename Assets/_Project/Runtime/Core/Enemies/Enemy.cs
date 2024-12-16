using System;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using static UnityEngine.Mathf;
using Zenject;

namespace _Project.Runtime.Core.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable, ITransformable
    {
        [SerializeField] private Collider _collider;
        [field: SerializeField] public Point Point { get; private set; }
        
        private Movement _movement;
        private Attack _attack;

        private EnemyAnimer _animer;

        private IHerbalistProvider _provider;

        [Inject]
        private void Construct(
            IHerbalistProvider provider, 
            EnemyAnimer animer, 
            Movement movement, 
            IHealth health,
            Attack attack)
        {
            _provider = provider;
            _animer = animer;
            _movement = movement;
            Health = health;
            _attack = attack;
        }
        public bool CloseEnoughToAttack => 
            Vector3.SqrMagnitude(transform.position - _provider.Herbalist.Transform.position) <= Pow(_attack.Distance, 2f);
        public bool InAttackCooldown => _attack.InCooldown;
        public IHealth Health { get; private set; }
        public Transform Transform => transform;

        public event Action<Enemy> Died;

        private void Start()
        {
            _animer.Hitted += OnHitted;
        }

        private void OnDestroy()
        {
            _animer.Hitted -= OnHitted;
        }

        private void OnHitted()
        {
            _movement.Resume();
        }
        
        public void TakeDamage(float value)
        {
            Health.Decrease(value);
            _animer.PlayHit();
            _movement.Pause();
            if (Health.Value.CurrentValue == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _collider.enabled = false;
            _animer.PlayDeath();
            Died?.Invoke(this);
        }

        public void Move()
        {
            _movement.Move(_provider.Herbalist.Transform.position);
        }

        public void Attack()
        {
            _attack.Execute(_provider.Herbalist);
        }

        public void Destroy()
        {
            
        }
    }
}