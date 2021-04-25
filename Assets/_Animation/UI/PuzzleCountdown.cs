using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleCountdown : StateMachineBehaviour
{
    private static float time = 90;

    private TextMeshPro text;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(animator.GetComponentInParent<Transform>());
        animator.GetComponentInParent<RecyclingBinsPuzzle>().EnableItems();
        text = animator.GetComponent<TMPro.TextMeshPro>();
        text.text = "Te quedan \n 90.00 \n segundos"; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = 0;
            animator.SetBool("exit", true);
        }
        text.text = "Te quedan\n" + time.ToString("0.00") + "\nsegundos";
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<RecyclingBinsPuzzle>().Verify();
    }

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
