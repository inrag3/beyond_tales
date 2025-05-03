using System;
using _Project.Runtime.Core.Enemies;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public class HerbalistAnimer : Animer
    {
        private static readonly int Roll = Animator.StringToHash("Roll");
        private static readonly int Equip = Animator.StringToHash("Equip");
        private static readonly int Disequip = Animator.StringToHash("Disequip");
        private event Action OnRollComplete;
        private event Action OnDisequipComplete;
        private event Action OnEquipComplete;

        public void PlayRoll(Action onComplete = null)
        {
            OnRollComplete = onComplete;
            _animator.SetBool(Roll, true);
            _animator.SetBool(Attack, false);
        }

        [UsedImplicitly]
        private void StopRoll()
        {
            OnRollComplete?.Invoke();
            _animator.SetBool(Roll, false);
        }

        public void PlayDisequip(Action onComplete = null)
        {
            OnDisequipComplete = onComplete;
            _animator.SetBool(Disequip, true);
        }

        [UsedImplicitly]
        private void StopDisequip()
        {
            OnDisequipComplete?.Invoke();
            _animator.SetBool(Disequip, false);
        }
        
        public void PlayEquip(Action onComplete = null)
        {
            OnEquipComplete = onComplete;
            _animator.SetBool(Disequip, true);
        }

        [UsedImplicitly]
        private void StopEquip()
        {
            OnEquipComplete?.Invoke();
            _animator.SetBool(Disequip, false);
        }

    }
}