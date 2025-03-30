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
        private PauseHandlersRegister _pauseHandlersRegister;
        private Rigidbody _rigidbody;
        private Mover _mover;
        private Attacker _attacker;
        private HerbalistAnimer _animer;
        private PlayerData _playerData;

        [Inject]
        private void Construct(IHealth health, IScanner<Interactable> scanner, Mover mover, 
            Attacker attacker, HerbalistAnimer animer, PlayerData playerData, PauseHandlersRegister pauseHandlersRegister)
        {
            _animer = animer;
            _attacker = attacker;
            _mover = mover;
            //TODO убрать 
            Scanner = scanner;
            Health = health;
            _playerData = playerData;
            _pauseHandlersRegister = pauseHandlersRegister;
            _pauseHandlersRegister.RegisterPauseHandler(this);
        }

        public IScanner<Interactable> Scanner { get; private set; }

        public IHealth Health { get; private set; }

        public Transform Transform => transform;

        public PlayerData PlayerData => _playerData;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            IDisposable subscription = Health.Value.Subscribe(OnHealthChanged);
            _disposables.Add(subscription);
        }

        private void OnHealthChanged(float value)
        {
            if (value > 0)
                return;
            
            _rigidbody.isKinematic = true;
            _animer.PlayDeath();
            _mover.Pause();
            _attacker.Pause();
        }

        public void TakeDamage(float value)
        {
            Health.Decrease(value);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void Pause()
        {
            _animer.Pause();
        }

        public void Resume()
        {
            _animer.Resume();
        }
    }
}