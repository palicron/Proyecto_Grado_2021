using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum LOOTING
{
    Low,Medium,Hight
}
public class Npc_waderer : NPC
{

    public List<GameObject> ArrayPoints;

    public bool IsRandomPatroling = false;

    public float RandomPatrolDistance = 50.0f;

    public LOOTING loot;
    NavMeshAgent agent;
    Animator anim;
    [SerializeField]
    LayerMask isRecoelPoint;
    [SerializeField]
    State CurrentState;
    [SerializeField]
    Material[] NpcMat;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (IsRandomPatroling)
        {

            RaycastHit[] raycast = Physics.SphereCastAll(this.transform.position, RandomPatrolDistance, transform.forward, 360.0f, isRecoelPoint);

            foreach (RaycastHit h in raycast)
            {
                ArrayPoints.Add(h.transform.gameObject);
            }
        }
        int ran = Random.Range(0, NpcMat.Length);

       
        GetComponentInChildren<Renderer>().material = NpcMat[ran];

        CurrentState = new Patrol_Wanderer(this.gameObject, agent, anim, 0);
    }

    void Update()
    {
        ManageINputs();
        CurrentState = CurrentState.Process();
        anim.SetFloat("HorizontalSpeed", (agent.velocity.magnitude / agent.speed));
     
    }

    public override void EndDialogue()
    {

        StopAllCoroutines();
        bIsInConversation = false;
        CurrentNearPlayer.SetDialogue(false, Vector3.zero);
       // anim.SetBool("IsTalking", false);
    }

    public override void midDialgueAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Interect()
    {
        TriggerDialogue();
    }

    protected override void TriggerDialogue()
    {

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

    // Start is called before the first frame update

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
