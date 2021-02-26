using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_StacinariNpc : NPC
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && IsPlayerInrange && !bIsInConversation)
        {
            Interect();

        }
        else if(Input.GetKeyDown(KeyCode.Z) && IsPlayerInrange && bIsInConversation)
        {
            DialogueManager.intance.DisplayNextSentence();
        }
        else if(Input.GetKeyDown(KeyCode.X) && IsPlayerInrange && bIsInConversation )
        {
           DialogueManager.Instance.EndDialgue();
        }
    }

    protected override void Interect()
    {
    
       TriggerDialogue();

    }

    protected override void TriggerDialogue()
    {
         CurrentNearPlayer.CanControlPlayer = false;
        Vector3 looktarget = CurrentNearPlayer.transform.position;
        looktarget.y = transform.position.y;
        lookAtTarget(looktarget);
        bIsInConversation = true;
        if(DialogueInRandomOrder)
        {
           int ran = Random.Range(0,Dialogues.Length);
           DialogueManager.intance.StarDialogue(Dialogues[ran],this);
        }
        else
        {
             DialogueManager.intance.StarDialogue(Dialogues[DialogueIndex],this);
        }
    }


    public override void EndDialogue()
    {
          bIsInConversation = false;
           CurrentNearPlayer.CanControlPlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        CurrentNearPlayer = other.gameObject.GetComponent<PlayerCtr>();
        if (CurrentNearPlayer)
        {
            IsPlayerInrange = true;
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCtr>())
        {

            CurrentNearPlayer = null;
            IsPlayerInrange = false;
        }
    }

}
