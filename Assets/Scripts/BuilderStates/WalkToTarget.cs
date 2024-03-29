﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalkToTarget : StateMachineBehaviour
{
    private Vector3 _target;
    private Builder _builderNPC;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _builderNPC = animator.gameObject.GetComponent<Builder>();

        if (_builderNPC.units.Count == 0)
            _target = _builderNPC._home.transform.position;
        else
        {
            _target = _builderNPC.units.Peek();
        }

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float step = _builderNPC.speed * Time.deltaTime;
        _builderNPC.transform.position = Vector3.MoveTowards(_builderNPC.transform.position, _target, step);

        if (_builderNPC.transform.position == _target & _target != _builderNPC._home.transform.position)
        {
            animator.SetBool("isBuilding", true);
            animator.SetBool("isWalking", false);
        }
        else if (_builderNPC.transform.position == _builderNPC._home.transform.position)
        {
            animator.SetBool("isWaiting", true);
            animator.SetBool("isWalking", false);
        }

    }

    ////OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

}
