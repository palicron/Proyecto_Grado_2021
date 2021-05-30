using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.GetComponent<Rigidbody>());
        ItemPickup item = animator.GetComponent<ItemPickup>();
        if(item !=null)
        {
            animator.GetComponent<Collider>().isTrigger = true;
            item.hover = true;
        }
        else
        {
            EntryPickup pItem = animator.GetComponentInParent<EntryPickup>();
            animator.GetComponentInParent<Collider>().isTrigger = true;
            pItem.hover = true;
        }
        animator.transform.Translate(0, 40F * Time.deltaTime,0);
        animator.transform.localRotation = Quaternion.Euler(35, 0, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
