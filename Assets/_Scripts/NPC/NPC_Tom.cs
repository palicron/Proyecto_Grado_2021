using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField]
    bool MoveAtTheEnd = false;
    [SerializeField]
    GameObject EndPoint;
    Animator anim;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = DistanceTotalk - 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        ManageINputs();
        Debug.Log(CurrentNearPlayer);
    }

    public override void EndDialogue()
    {


        StopAllCoroutines();
        if (MoveAtTheEnd)
        {

        }
        else
        {
            bIsInConversation = false;
            CurrentNearPlayer.SetDialogue(false, Vector3.zero);
            anim.SetBool("IsTalking", false);
        }

    }

    protected override void Interect()
    {
        TriggerDialogue();
        Debug.Log(CurrentNearPlayer);
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


        if (other.gameObject.GetComponent<PlayerCtr>())
        {
            CurrentNearPlayer = other.gameObject.GetComponent<PlayerCtr>();
            IsPlayerInrange = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCtr>())
        {
            Debug.Log("Me sali");
            CurrentNearPlayer = null;
            IsPlayerInrange = false;
        }
    }


    IEnumerator WalkToplayer()
    {
        CurrentNearPlayer.SetDialogue(true, this.transform.position);
        bIsInConversation = true;
        agent.SetDestination(CurrentNearPlayer.transform.position);
        // while (Vector3.Distance(this.transform.position, CurrentNearPlayer.transform.position) > DistanceTotalk)
        //{
        while (agent.remainingDistance > DistanceTotalk)
        {

            //Vector3 looktarget = CurrentNearPlayer.transform.position;
            //looktarget.y = transform.position.y;
            //lookAtTarget(looktarget);
            //rb.AddForce(transform.forward.normalized * Speed * Time.deltaTime, ForceMode.VelocityChange);
            agent.SetDestination(CurrentNearPlayer.transform.position);
            // Vector3 rr = rb.velocity;
            // rr.y = 0;
            // if (rr.magnitude >= MaxSpeed)
            // {
            //   Vector3 NewSpeed = rb.velocity.normalized * MaxSpeed;
            //  NewSpeed.y = rb.velocity.y;
            // rb.velocity = NewSpeed;
            anim.SetFloat("Speed", (agent.velocity.magnitude / agent.speed));
            //}
            yield return new WaitForEndOfFrame();
        }


        anim.SetFloat("Speed", 0);


        TriggerDialogue();
    }

    public override void midDialgueAction()
    {
        throw new System.NotImplementedException();
    }


}
