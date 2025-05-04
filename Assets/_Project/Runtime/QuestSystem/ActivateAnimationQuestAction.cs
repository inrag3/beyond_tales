using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class ActivateAnimationQuestAction : BaseQuestAction
    {
        [SerializeField] Animator _animator;
        
        public override void Activate()
        {
            _animator.SetTrigger("Summoning");
            // _animator.ResetTrigger("Summoning");
        }
    }
}