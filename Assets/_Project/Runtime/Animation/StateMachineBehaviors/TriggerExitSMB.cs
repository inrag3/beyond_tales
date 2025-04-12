using System.Collections;
using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using UnityEngine;

public class TriggerExitSMB : StateMachineBehaviour
{
    [SerializeField] private float _normalizedTimeToTransition;

    private bool _triggered;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _triggered = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!_triggered && stateInfo.normalizedTime > _normalizedTimeToTransition)
        {
            if (animator.TryGetComponent<Herbalist>(out var herbalist))
            {
                animator.GetComponent<Animer>().OnPlayedAttack();
                _triggered = true;
            }
        }

        if (animator.IsInTransition(0))
        {
            //Debug.Log($"confirm in transition");
            animator.GetComponent<Animer>().ConfirmExitAttackState();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /*if (animator.TryGetComponent<Herbalist>(out var herbalist))
        {
            animator.GetComponent<Animer>().ConfirmExitAttackState();
        }*/
    }

   
}
