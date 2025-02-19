using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public interface IInputService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool IsRollButtonPressed { get; }
        
        public bool IsPotionApplyButtonPressed { get; }
        public bool IsPotionNextButtonPressed { get; }
        public bool IsPotionPreviousButtonPressed { get; }

        public bool IsInteractButtonPressed { get; }
        public bool IsDialogButtonPressed { get; }

        public bool IsAttackButtonPressed { get; }
        public Vector3 Mouse { get; }
    }
}