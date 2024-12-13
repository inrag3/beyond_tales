using System;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.PauseHandler;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class Enemy : MonoBehaviour, IDamageable, ITransformable, IPauseHandler
    {
        private IHerbalistProvider _herbalistProvider;

        [Inject]
        private void Construct(IHerbalistProvider herbalistProvider, IHealth health)
        {
            Health = health;
            _herbalistProvider = herbalistProvider;
        }
        public IHealth Health { get; private set; }
        public Transform Transform => transform;
        public event Action<Enemy> Died;

        public void TakeDamage(int value)
        {
            Health.Decrease(value);
            if (Health.Value.CurrentValue == 0)
            {
                Died?.Invoke(this);
            }
        }
        
        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void Destroy()
        {
            
        }
    }
}