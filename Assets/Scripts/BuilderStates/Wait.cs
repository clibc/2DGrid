﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : StateMachineBehaviour
{
    Builder _builderNPC;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _builderNPC = animator.gameObject.GetComponent<Builder>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_builderNPC.units.Count > 0)
        {
            animator.SetBool("isWaiting", false);
            animator.SetBool("isWalking", true);
        }
    }

    //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

}
