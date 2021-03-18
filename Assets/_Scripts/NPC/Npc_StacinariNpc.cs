using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_StacinariNpc : NPC
{

    Animator anim;
    Quaternion StarRotation;

    public LOOTING loot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        switch(loot)
        {
            case LOOTING.Low:
                anim.SetInteger("Looting", 1);
                break;
            case LOOTING.Medium:
                anim.SetInteger("Looting", 2);
                break;
            case LOOTING.Hight:
                anim.SetInteger("Looting", 3);
                break;

        }
     
        StarRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ManageINputs();
    }

    protected override void Interect()
    {

        TriggerDialogue();

    }

    protected override void TriggerDialogue()
    {
        CurrentNearPlayer.SetDialogue(true, this.transform.position,this.transform);
        if(SeePlayerWhenTalking)
        {
            Vector3 looktarget = CurrentNearPlayer.transform.position;
            looktarget.y = transform.position.y;
            lookAtTarget(looktarget);
            anim.SetBool("Talking", true);
        }
     
        bIsInConversation = true;
        if (DialogueInRandomOrder)
        {
            int ran = Random.Range(0, Dialogues.Length);
            DialogueManager.intance.StarDialogue(Dialogues[ran], this);
        }
        else
        {
            DialogueManager.intance.StarDialogue(Dialogues[DialogueIndex], this);
        }
    }


    public override void EndDialogue()
    {
        bIsInConversation = false;
        CurrentNearPlayer.SetDialogue(false, Vector3.zero);
        transform.rotation = StarRotation;
        anim.SetBool("Talking", false);
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

    public override void midDialgueAction()
    {
        throw new System.NotImplementedException();
    }
}
