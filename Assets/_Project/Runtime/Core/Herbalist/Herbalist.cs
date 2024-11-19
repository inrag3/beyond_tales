using System;
using _Project.Runtime.Core.Health;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class Herbalist : MonoBehaviour, IHerbalist
    {
        private readonly CompositeDisposable _disposables = new();
        private Rigidbody _rigidbody;
        private Animer _animer;
        private Mover _mover;

        [Inject]
        private void Construct(IHealth health, Animer animer)
        {
            Health = health;
            _animer = animer;
        }

        public IHealth Health { get; private set; }

        public Transform Transform => transform;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            IDisposable subscription = Health.Value.Subscribe(OnHealthChanged);
            _disposables.Add(subscription);
        }

        private void OnHealthChanged(int value)
        {
            if (value > 0)
                return;

            _animer.PlayDeath();
            _rigidbody.isKinematic = true;
        }

        public void TakeDamage(int value)
        {
            Health.Decrease(value);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}