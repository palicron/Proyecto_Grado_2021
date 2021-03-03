using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Tom : NPC
{
    [SerializeField]
    bool WalksToPlater = false;
    Rigidbody rb;
    [SerializeField]
    float Speed = 10.0f;
    [SerializeField]
    float DistanceTotalk = 20.0f;
    [SerializeField]
    float MaxSpeed = 4;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageINputs();
    }

    public override void EndDialogue()
    {
        bIsInConversation = false;
        CurrentNearPlayer.SetDialogue(false, Vector3.zero);
        anim.SetBool("IsTalking", false);
    }

    protected override void Interect()
    {
        TriggerDialogue();
    }

    protected override void TriggerDialogue()
    {
        anim.SetBool("IsTalking", true); 
        
        CurrentNearPlayer.SetDialogue(true, this.transform.position);
        Vector3 looktarget = CurrentNearPlayer.transform.position;
        looktarget.y = transform.position.y;
        lookAtTarget(looktarget);
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

    public void EstarConbersationEvent(PlayerCtr Player)
    {
        CurrentNearPlayer = Player;

        if (!WalksToPlater)
        {
            TriggerDialogue();
        }
        else
        {
            StartCoroutine(WalkToplayer());
        }
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


    IEnumerator WalkToplayer()
    {
        CurrentNearPlayer.SetDialogue(true, this.transform.position);  
        bIsInConversation = true;
        while (Vector3.Distance(this.transform.position, CurrentNearPlayer.transform.position) > DistanceTotalk)
        {

            Vector3 looktarget = CurrentNearPlayer.transform.position;
            looktarget.y = transform.position.y;
            lookAtTarget(looktarget);
            rb.AddForce(transform.forward.normalized * Speed * Time.deltaTime, ForceMode.VelocityChange);
         
            Vector3 rr = rb.velocity;
            rr.y = 0;
            if (rr.magnitude >= MaxSpeed)
            {
                Vector3 NewSpeed = rb.velocity.normalized * MaxSpeed;
                NewSpeed.y = rb.velocity.y;
                rb.velocity = NewSpeed;
                anim.SetFloat("Speed", rb.velocity.magnitude/ MaxSpeed);
            }
            yield return new WaitForEndOfFrame();
        }
        
        rb.velocity = Vector3.zero;
        anim.SetFloat("Speed", 0);


        TriggerDialogue();
    }

}
