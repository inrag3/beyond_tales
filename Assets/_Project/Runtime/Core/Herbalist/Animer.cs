using System;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public sealed class Animer
    {
        private static readonly int Running = Animator.StringToHash("Running");
        private readonly Animator _animator;

        public Animer(Animator animator)
        {
            _animator = animator;
        }

        public void PlayDeath()
        {
        }

        public void PlayMove(float value)
        {
            _animator.SetFloat(Running, value);
        }
    }
}