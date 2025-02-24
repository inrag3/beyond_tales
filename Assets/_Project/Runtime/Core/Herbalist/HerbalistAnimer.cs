using System;
using _Project.Runtime.Core.Enemies;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public class HerbalistAnimer : Animer
    {
        private static readonly int Roll = Animator.StringToHash("Roll");
        private Action _onComplete;

        public void PlayRoll(Action onComplete = null)
        {
            _onComplete = onComplete;
            Animator.SetBool(Roll, true);
            Animator.SetBool(Attack, false);
        }
        
        [UsedImplicitly]
        private void StopRoll()
        {
            _onComplete?.Invoke();
            Animator.SetBool(Roll, false);
        }
    }
}