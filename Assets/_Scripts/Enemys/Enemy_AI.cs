using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_AI : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    healthsystems NpcHealth;
    Rigidbody rb;
    public Transform player;
    [SerializeField]
    float Damage = 10.0f;
    [SerializeField]
    public float Touchdis = 4.0f;
    public List<GameObject> ArrayPoints;
    public GameObject lastCheckPoint;
    [SerializeField]
    State CurrentState;
    public bool IsRandomPatroling = true;
    public float RandomPatrolDistance = 50.0f;
    [SerializeField]
    LayerMask isRecoelPoint;
    [SerializeField]
    Material[] NpcMat;
    public bool IsPaytoling = false;
    bool isAttacking = false;
    public bool IsLooting = false;
    public GameObject DamgeTrigger;
    bool alive = true;
    bool dmgT = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        NpcHealth = GetComponent<healthsystems>();

       
        if (IsRandomPatroling)
        {
            
            RaycastHit[] raycast = Physics.SphereCastAll(this.transform.position, RandomPatrolDistance, transform.forward, 360.0f,isRecoelPoint);
           
            foreach (RaycastHit h in raycast)
            {
                ArrayPoints.Add(h.transform.gameObject);
            }
        }

        int ran = Random.Range(0, NpcMat.Length);


        GetComponentInChildren<Renderer>().material = NpcMat[ran];

        CurrentState = new Idle(this.gameObject, agent, anim, player, NpcHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            CurrentState = CurrentState.Process();
            anim.SetFloat("Speed", (agent.velocity.magnitude / 4.5f));
            if((player.position -this.transform.position).magnitude<Touchdis && CurrentState.name != State.STATE.ATTACKING)
            {
                CurrentState.nextState = new Attack(this.gameObject, agent, anim, player, NpcHealth);
                CurrentState.stage = State.EVENT.EXIT;

            }
        }
  
    }

    public void Attack()
    {
        if (isAttacking)
            return;

        isAttacking = true;
        anim.SetTrigger("Attack");

    }

    public void resetAttack()
    {
        isAttacking = false;
        anim.ResetTrigger("Attack");
    }
    public void resetLooting()
    {
        IsLooting = false;
        anim.ResetTrigger("LootingUp");
        anim.ResetTrigger("LootingLow");
    }

    public void death()
    {
        anim.SetTrigger("Die");
        alive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Playerhealthsystems hs = other.gameObject.GetComponent<Playerhealthsystems>();
        //if(hs)
        //{
     
          //  hs.TakeDmg(Damage);
        //}
         
    }

    public void ToggleAttack()
    {
        dmgT = !dmgT;
        DamgeTrigger.SetActive(dmgT);
    }
}
