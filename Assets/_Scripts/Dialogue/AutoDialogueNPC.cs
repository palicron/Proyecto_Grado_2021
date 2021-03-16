using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogueNPC : NPC
{
    bool Ontime = true;
    public override void EndDialogue()
    {
        throw new System.NotImplementedException();
    }

    public override void midDialgueAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Interect()
    {
        throw new System.NotImplementedException();
    }

    protected override void TriggerDialogue()
    {
        bIsInConversation = true;
        DialogueManager.intance.AutomaticDialogue(Dialogues[DialogueIndex], this);
        if(Ontime)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            TriggerDialogue();
            
        }
    }

}
