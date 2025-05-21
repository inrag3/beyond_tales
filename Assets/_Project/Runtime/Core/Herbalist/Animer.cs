using System;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public abstract class Animer : MonoBehaviour
    {
        private static readonly int Running = Animator.StringToHash("Running");
        protected static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly string Hit = "Hit";
        private static readonly string Idle = "Idle";
        private const string Death = "Death";

        private int startAttack = 0;
        private int endAttack = 0;
        
        protected Animator _animator;

        public Animator Animator
        {
            get => _animator;
            set => _animator = value;
        }
        private event Action _onAttackComplete;
        public event Action Attacked;
        public event Action Hitted;

        private bool _attackExitConfirmed = true;

        [Inject]
        private void Construct(Animator animator)
        {
            _animator = animator;
        }
        
        public void PlayDeath()
        {
            _animator.Play(Death);
        }

        public void PlayMove(float value)
        {
            _animator.SetFloat(Running, value);
        }

        public bool isPlayingHit() => _animator.GetCurrentAnimatorStateInfo(0).IsName(Hit);

        private void Update()
        {
            
        }

        public void PlayAttack()
        {
            if (!_animator.GetBool(Attack))
            {
                startAttack += 1;
                //Debug.Log($"start attack count = {startAttack}");
                _animator.SetBool(Attack, true);
            }
        }

        public void Pause()
        {
            _animator.speed = 0;
        }

        public void Resume()
        {
            _animator.speed = 1;
        }

        public void PlayAttack(Action onComplete)
        {
            _onAttackComplete = onComplete;
            _animator.SetBool(Attack, true);
        }

        public void PlayHit()
        {
            
            //Debug.Log($"Play hit");
            _animator.Play(Hit);
        }

        public void PlayIdle()
        {
            _animator.SetTrigger(Idle);
        }
        
        public void SetBool(string flagName, bool flag)
        {
            _animator.SetBool(flagName, flag);
        }

        public void SetTrigger(string trigger)
        {
            _animator.SetTrigger(trigger);
        }

        [UsedImplicitly]
        public void OnPlayedAttack()
        {
            endAttack += 1;
            //Debug.Log($"end attack count = {endAttack}");
            if (_animator.GetBool(Attack))
            {
                _animator.SetBool(Attack, false);
                _attackExitConfirmed = false;
                _onAttackComplete?.Invoke();
                Attacked?.Invoke();
            }
        }
        
        [UsedImplicitly]
        private void OnPlayedHit()
        {
            _animator.SetBool(Hit, false);
            Hitted?.Invoke();
        }

        public bool IsAttacking()
        {
            return _animator.GetBool(Attack) || !_attackExitConfirmed;
        }

        public void ConfirmExitAttackState()
        {
            _attackExitConfirmed = true;
        }
    }
}