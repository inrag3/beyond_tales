using System;
using _Project.Runtime.Core.Herbalist;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Enemies
{
    public class Attack : MonoBehaviour
    {
        [field: SerializeField] public float Distance { get; private set; } = 1.5f;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _currentCooldown;
        private EnemyAnimer _animer;

        [Inject]
        private void Construct(EnemyAnimer animer)
        {
            _animer = animer;
        }
        public bool InCooldown => _currentCooldown > .05f;
        
        private void Update() => 
            _currentCooldown = Mathf.Max(_currentCooldown - Time.deltaTime, 0f);

        public void Execute(IDamageable damageable)
        {
            if (InCooldown)
                throw new Exception("Attempt to attack in cooldown!");
            
            _animer.PlayAttack(() =>
            {
                damageable.TakeDamage(2f);
            });
            _currentCooldown = _cooldown;
        }
    }
}