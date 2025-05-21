using System;
using System.Collections;
using _Project.Runtime.Audio;
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
        private IAudioService _audioService;
        
        private Renderer[] _cashRenderers;

        public event Action OnDeath;

        [Inject]
        private void Construct(IHealth health, IScanner<Interactable> scanner, Mover mover, 
            Attacker attacker, HerbalistAnimer animer, PlayerData playerData, 
            PauseHandlersRegister pauseHandlersRegister, IAudioService audioService)
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
            _audioService = audioService;
        }

        public IScanner<Interactable> Scanner { get; private set; }

        public IHealth Health { get; private set; }

        public Transform Transform => transform;

        public PlayerData PlayerData => _playerData;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _cashRenderers = GetComponentsInChildren<Renderer>();
        }

        private void OnEnable()
        {
            IDisposable subscription = Health.Value.Subscribe(OnHealthChanged);
            _disposables.Add(subscription);
        }

        private void OnHealthChanged(float value)
        {
            // if (value < Health.MaxValue - 10)
            // {
            //     Health.Increase(Health.MaxValue - 10 - value);
            // }
            if (value > 0)
                return;
            
            _rigidbody.isKinematic = true;
            _animer.PlayDeath();
            _mover.Pause();
            _attacker.Pause();
            OnDeath?.Invoke();
        }

        private void Update()
        {
            if (_mover.IsMoving)
            {
                _audioService.PlayWalkSound();
            }
            else
            {
                _audioService.StopWalkSound();
            }
            Debug.Log($"herbalist position = {transform.position}");
        }

        public void TakeDamage(float value)
        {
            Health.Decrease(value);
            HitFeedback();
        }
        
        private void HitFeedback()
        {
            foreach (var renderer in _cashRenderers)
            {
                renderer.material.color = Color.red;
            }
            StopCoroutine(RecoverDefaultColor());
            StartCoroutine(RecoverDefaultColor());
        }


        private IEnumerator RecoverDefaultColor()
        {
            yield return new WaitForSeconds(0.3f);
            
            foreach (var renderer in _cashRenderers)
            {
                renderer.material.color = Color.white;
            }
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void Pause()
        {
            _animer.Pause();
            _mover.Pause();
        }

        public void Resume()
        {
            _animer.Resume();
            _mover.Resume();
        }
        
        public void Teleport(Vector3 pos)
        {
            StartCoroutine(Teleportation(pos));
        }

        private IEnumerator Teleportation(Vector3 pos)
        {
            _mover.InTeleport = true;
            yield return new WaitForSeconds(0.1f);
            transform.position = pos;
            yield return new WaitForSeconds(0.1f);
            _mover.InTeleport = false;
        }
    }
}