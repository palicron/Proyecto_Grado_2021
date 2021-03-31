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
    float DistanceTotalk = 20.0f;
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
      
        anim.SetFloat("HorizontalSpeed", (agent.velocity.magnitude / agent.speed));
       
       

    }

    public override void EndDialogue()
    {


        StopAllCoroutines();
     
        if (MoveAtTheEnd)
        {
            anim.SetBool("Talking", false);
            StartCoroutine(walkToPoint());
        }
        else
        {
            bIsInConversation = false;
            CurrentNearPlayer.SetDialogue(false, Vector3.zero);
            anim.SetBool("Talking", false);
        }

    }

    protected override void Interect()
    {
        TriggerDialogue();
       
    }

    protected override void TriggerDialogue()
    {
        anim.SetBool("Talking", true);

        CurrentNearPlayer.SetDialogue(true, this.transform.position);
        CurrentNearPlayer.cancelAllMovment();
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

    public void starConbersationEvent(PlayerCtr Player)
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
        if (other.gameObject.GetComponent<PlayerCtr>() && !bIsInConversation)
        {
        
            CurrentNearPlayer = null;
            IsPlayerInrange = false;
        }
    }


    IEnumerator WalkToplayer()
    {
        CurrentNearPlayer.SetDialogue(true, this.transform.position);
        bIsInConversation = true;
        agent.SetDestination(CurrentNearPlayer.transform.position);
   
        while ((CurrentNearPlayer.transform.position-transform.position).magnitude > DistanceTotalk)
        {

           
            agent.SetDestination(CurrentNearPlayer.gameObject.transform.position);
            yield return new WaitForEndOfFrame();
        }
        anim.SetFloat("HorizontalSpeed", 0);
        agent.SetDestination(this.gameObject.transform.position);
        TriggerDialogue();
    }

    IEnumerator walkToPoint()
    {
        agent.SetDestination(EndPoint.transform.position);
        while((EndPoint.transform.position - transform.position).magnitude >3)
        {
           
            yield return new WaitForEndOfFrame();
        }
        StopAllCoroutines();
        CurrentNearPlayer.SetDialogue(false, Vector3.zero);
        Destroy(this.gameObject);
      
    }

    public override void midDialgueAction()
    {
        throw new System.NotImplementedException();
    }


}
