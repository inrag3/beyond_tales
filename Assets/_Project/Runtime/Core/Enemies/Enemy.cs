using System;
using _Project.Runtime.Infrastructure.Factories;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Mathf;
using Zenject;

namespace _Project.Runtime.Core.Enemies
{
    public class Enemy : Creature
    {
        [SerializeField] private Collider _collider;
        [field: SerializeField] public Point Point { get; private set; }
        
        private Movement _movement;
        private IAttacker _attack;

        private EnemyAnimer _animer;
        public event Action<Enemy> Died;
        
        private IHerbalistProvider _provider;
        
        [Inject]
        private void Construct(
            IHerbalistProvider provider, 
            EnemyAnimer animer, 
            Movement movement, 
            Attack attack)
        {
            _provider = provider;
            _animer = animer;
            _movement = movement;
            _attack = attack;
        }
        public bool CloseEnoughToAttack => 
            Vector3.SqrMagnitude(transform.position - _provider.Herbalist.Transform.position) <= Pow(_attack.Distance, 2f);
        public bool InAttackCooldown => _attack.InCooldown;

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
        
        public override void TakeDamage(float value)
        {
            _animer.PlayHit();
            _movement.Pause();
            base.TakeDamage(value);
        }

        protected override void Die()
        {
            _collider.enabled = false;
            _animer.PlayDeath();
            transform.DOScale(Vector3.zero, 0.15f);
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