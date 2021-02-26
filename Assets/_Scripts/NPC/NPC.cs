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


    protected abstract void Interect();

    protected abstract void TriggerDialogue();

    public abstract void EndDialogue();

    protected virtual void lookAtTarget(Vector3 target)
    {
        //nesesita ahcerse que vovle smoot
        transform.LookAt(target);
    }
}
