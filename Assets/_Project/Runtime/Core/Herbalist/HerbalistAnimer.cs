using _Project.Runtime.Core.Enemies;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public class HerbalistAnimer : Animer
    {
        private static readonly int Roll = Animator.StringToHash("Roll");
        public void PlayRoll()
        {
            Animator.SetBool(Roll, true);
            Animator.SetBool(Attack, false);
        }
        
        [UsedImplicitly]
        private void StopRoll()
        {
            Animator.SetBool(Roll, false);
        }
    }
}