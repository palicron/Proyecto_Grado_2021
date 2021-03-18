using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class NPC : MonoBehaviour
{

    [SerializeField]
    protected SO_Dialogue[] Dialogues;

    protected bool IsPlayerInrange;

    protected int DialogueIndex = 0;

    [SerializeField]
    protected bool DialogueInRandomOrder =false;
    [SerializeField]
   protected bool SeePlayerWhenTalking = true;

    protected bool bIsInConversation =false;

    protected PlayerCtr CurrentNearPlayer;

 


    protected virtual void ManageINputs()
    {
        if (Input.GetButtonDown("Interact") && IsPlayerInrange && !bIsInConversation && !DialogueManager.IsinConversation)
        {
            Interect();

        }
        else if (Input.GetButtonDown("Interact")  && bIsInConversation && !DialogueManager.WaitingAswer)
        {
            DialogueManager.intance.DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.C) && IsPlayerInrange && bIsInConversation)
        {
            DialogueManager.Instance.EndDialgue();
        }
    }
    protected abstract void Interect();

    protected abstract void TriggerDialogue();

    public abstract void EndDialogue();

    public abstract void midDialgueAction();





    protected virtual void lookAtTarget(Vector3 target)
    {
        //nesesita ahcerse que vovle smoot
        transform.LookAt(target);
    }

    public bool IsTalking()
    {
        return bIsInConversation;
    }
}
