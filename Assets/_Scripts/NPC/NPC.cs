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

    protected bool bIsInConversation =false;

    protected PlayerCtr CurrentNearPlayer;

    protected virtual void ManageINputs()
    {
        if (Input.GetButtonDown("Interact") && IsPlayerInrange && !bIsInConversation)
        {
            Interect();

        }
        else if (Input.GetButtonDown("Interact") && IsPlayerInrange && bIsInConversation && !DialogueManager.WaitingAswer)
        {
            DialogueManager.intance.DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.X) && IsPlayerInrange && bIsInConversation)
        {
            DialogueManager.Instance.EndDialgue();
        }
    }
    protected abstract void Interect();

    protected abstract void TriggerDialogue();

    public abstract void EndDialogue();

    protected virtual void lookAtTarget(Vector3 target)
    {
        //nesesita ahcerse que vovle smoot
        transform.LookAt(target);
    }
}
