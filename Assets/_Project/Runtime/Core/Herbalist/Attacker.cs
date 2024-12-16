using System;
using _Project.Runtime.Core.Enemies;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class Attacker : ITickable, IInitializable, IDisposable
    {
        private readonly LayerMask _layerMask = 1 << 7;
        private readonly IInputService _inputService;
        private readonly HerbalistAnimer _animer;
        private readonly Transform _transform;
        private readonly Detector _detector;
        private readonly Mover _mover;
        private Enemy _target;

        public Attacker(
            IInputService inputService, 
            HerbalistAnimer animer, 
            Transform transform,
            Detector detector,
            Mover mover)
        {
            _mover = mover;
            _detector = detector;
            _transform = transform;
            _inputService = inputService;
            _animer = animer;
        }

        public void Initialize()
        {
            _animer.Attacked += OnAttacked;
        }

        private void OnAttacked()
        {
            if (_target != null)
                _target.TakeDamage(10);
        }
        public void Dispose()
        {
            _animer.Attacked -= OnAttacked;
        }
        public void Tick()
        {
            if (!_inputService.IsAttackButtonPressed)
                return;

            _animer.PlayAttack();
            
            _target = _detector.Target;
            if (_target == null)
                return;
            
            _mover.Pause();
            
            _transform.DOLookAt(_target.transform.position, 0.2f).OnComplete(() =>
            {
                _mover.Resume();
            });
        }
    }
}