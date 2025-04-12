using System;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.PauseHandler;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class Attacker : ITickable, IInitializable, IDisposable, IPauseHandler
    {
        private readonly IInputService _inputService;
        private readonly HerbalistAnimer _animer;
        private readonly Transform _transform;
        private readonly Detector _detector;
        private readonly Mover _mover;
        private readonly PauseHandlersRegister _pauseHandlersRegister;
        private Creature _target;
        private bool _isPause;

        public Attacker(
            IInputService inputService, 
            HerbalistAnimer animer, 
            Transform transform,
            Detector detector,
            Mover mover,
            PauseHandlersRegister pauseHandlersRegister)
        {
            _mover = mover;
            _detector = detector;
            _transform = transform;
            _inputService = inputService;
            _animer = animer;
            _pauseHandlersRegister = pauseHandlersRegister;
            _pauseHandlersRegister.RegisterPauseHandler(this);
        }

        public void Initialize()
        {
            _animer.Attacked += OnAttacked;
        }

        private void OnAttacked()
        {
            if (_target != null)
                _target.TakeDamage(25);
        }
        public void Dispose()
        {
            _animer.Attacked -= OnAttacked;
        }
        public void Tick()
        {
            if (_isPause || !_inputService.IsAttackButtonPressed)
                return;

            if (!_animer.IsAttacking())
            {
                _animer.PlayAttack();
            }

            _target = _detector.Target;
            if (_target == null)
                return;
            
            _mover.Pause();
            
            _transform.DOLookAt(_target.transform.position, 0.2f).OnComplete(() =>
            {
                _mover.Resume();
            });
        }

        public void Pause()
        {
            _isPause = true;
        }

        public void Resume()
        {
            _isPause = false;
        }
    }
}