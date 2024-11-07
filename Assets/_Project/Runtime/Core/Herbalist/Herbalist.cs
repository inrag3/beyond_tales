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
        private IHealth _health;

        [Inject]
        private void Construct(IHealth health, Animer animer)
        {
            _health = health;
            _animer = animer;
        }

        public Transform Transform => transform;

        private void OnEnable()
        {
            IDisposable subscription = _health.Value.Subscribe(OnHealthChanged);
            _disposables.Add(subscription);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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

            _health.Increase(value);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }


    }
}