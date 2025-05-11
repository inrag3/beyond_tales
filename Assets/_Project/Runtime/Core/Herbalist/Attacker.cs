using System;
using System.Collections.Generic;
using _Project.Runtime.Audio;
using _Project.Runtime.Config;
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
        private readonly Mover _mover;
        private readonly PauseHandlersRegister _pauseHandlersRegister;
        private readonly IAttackConfig _herbalistAttackConfig;
        private HashSet<Creature> _targets = new();
        private bool _isPause;

        private IAudioService _audioService;
        private SoundSettings _soundSettings;
        public Attacker(
            IInputService inputService,
            HerbalistAnimer animer,
            Transform transform,
            Mover mover,
            PauseHandlersRegister pauseHandlersRegister,
            IAttackConfig herbalistAttackConfig, IAudioService audioService, SoundSettings soundSettings)
        {
            _mover = mover;
            _transform = transform;
            _inputService = inputService;
            _animer = animer;
            _pauseHandlersRegister = pauseHandlersRegister;
            _pauseHandlersRegister.RegisterPauseHandler(this);
            _herbalistAttackConfig = herbalistAttackConfig;
            _audioService = audioService;
            _soundSettings = soundSettings;
        }

        public void Initialize()
        {
            _animer.Attacked += OnAttacked;
        }

        private void OnAttacked()
        {
            _audioService.PlayOneShot(_soundSettings.fightClip);
            foreach (var creature in _targets)
            {
                creature.TakeDamage(_herbalistAttackConfig.HerbalistDamage);
            }
        }

        public void AddAttackedCreature(Creature creature)
        {
            _targets.Add(creature);
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
                _targets.Clear();
                _animer.PlayAttack();
                _mover.Pause();
                _transform.DOLookAt(_inputService.Mouse, 0.2f).OnComplete(() => { _mover.Resume(); });
            }

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