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
     
        protected Animator Animator;
        private event Action _onAttackComplete;
        public event Action Attacked;
        public event Action Hitted;

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

        public void PlayAttack()
        {
             Animator.SetBool(Attack, true);
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
            Animator.Play(Hit);
        }
        
        [UsedImplicitly]
        private void OnPlayedAttack()
        {
            Animator.SetBool(Attack, false);
            _onAttackComplete?.Invoke();
            Attacked?.Invoke();
        }
        
        [UsedImplicitly]
        private void OnPlayedHit()
        {
            Animator.SetBool(Hit, false);
            Hitted?.Invoke();
        }
    }
}