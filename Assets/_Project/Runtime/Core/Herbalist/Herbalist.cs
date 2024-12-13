using System;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.PauseHandler;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class Herbalist : MonoBehaviour, IHerbalist, IPauseHandler
    {
        private readonly CompositeDisposable _disposables = new();
        private Rigidbody _rigidbody;
        private Animer _animer;
        private Mover _mover;

        [Inject]
        private void Construct(IHealth health, Animer animer, IScanner<Interactable> scanner)
        {
            Scanner = scanner;
            Health = health;
            _animer = animer;
        }

        public IScanner<Interactable> Scanner { get; private set; }

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

        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }
    }
}