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
    public List<GameObject> ArrayPoints;
    public GameObject lastCheckPoint;
    [SerializeField]
    State CurrentState;
    public bool IsRandomPatroling = true;
    public float RandomPatrolDistance = 50.0f;
    [SerializeField]
    LayerMask isRecoelPoint;

    public bool IsPaytoling = false;
    bool isAttacking = false;
    public bool IsLooting = false;
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

        CurrentState = new Idle(this.gameObject, agent, anim, player, NpcHealth);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState = CurrentState.Process();
        anim.SetFloat("Speed", (agent.velocity.magnitude / agent.speed));
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

    private void OnTriggerEnter(Collider other)
    {
        Playerhealthsystems hs = other.gameObject.GetComponent<Playerhealthsystems>();
        if(hs)
        {
     
            hs.TakeDmg(Damage);
        }
         
    }
}
