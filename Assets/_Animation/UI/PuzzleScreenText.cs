using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScreenTexts : StateMachineBehaviour
{

    static string[] dialogs = { "Acomoda cada ítem correctamente en las canecas.", "¿Estás listo?", "3", "2", "1" };

    static int index;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (index == null)
        {
            index = 0;
        }
        animator.GetComponent<TMPro.TextMeshPro>().text = dialogs[index];
        index++;
    }
}
