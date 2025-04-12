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
        private static readonly int Hit = Animator.StringToHash("Hit");
        private const string Death = "Death";

        private int startAttack = 0;
        private int endAttack = 0;
        
        protected Animator Animator;
        private event Action _onAttackComplete;
        public event Action Attacked;
        public event Action Hitted;

        private bool _attackExitConfirmed = true;

        [Inject]
        private void Construct(Animator animator)
        {
            Animator = animator;
        }
        
        public void PlayDeath()
        {
            Animator.Play(Death);
        }

        public void PlayMove(float value)
        {
            Animator.SetFloat(Running, value);
        }

        private void Update()
        {
            
        }

        public void PlayAttack()
        {
            if (!Animator.GetBool(Attack))
            {
                startAttack += 1;
                //Debug.Log($"start attack count = {startAttack}");
                Animator.SetBool(Attack, true);
            }
        }

        public void Pause()
        {
            Animator.speed = 0;
        }

        public void Resume()
        {
            Animator.speed = 1;
        }

        public void PlayAttack(Action onComplete)
        {
            _onAttackComplete = onComplete;
            Animator.SetBool(Attack, true);
        }

        public void PlayHit()
        {
            
            //Debug.Log($"Play hit");
            Animator.Play(Hit);
        }
        
        [UsedImplicitly]
        public void OnPlayedAttack()
        {
            endAttack += 1;
            //Debug.Log($"end attack count = {endAttack}");
            if (Animator.GetBool(Attack))
            {
                Animator.SetBool(Attack, false);
                _attackExitConfirmed = false;
                _onAttackComplete?.Invoke();
                Attacked?.Invoke();
            }
        }
        
        [UsedImplicitly]
        private void OnPlayedHit()
        {
            Animator.SetBool(Hit, false);
            Hitted?.Invoke();
        }

        public bool IsAttacking()
        {
            return Animator.GetBool(Attack) || !_attackExitConfirmed;
        }

        public void ConfirmExitAttackState()
        {
            _attackExitConfirmed = true;
        }
    }
}