using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Patrol : State
{
    int currentIndex = 0;
    float NpcSpeed = 2.0f;
    List<GameObject> waypoints;
    bool NeedTocheck = false;
    bool FirtPoint = false;
   
    Enemy_AI ai;
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, healthsystems _heal) : base(_npc, _agent, _anim, _player, _heal)
    {
        name = STATE.PATROLING;
        agent.speed = NpcSpeed;
        agent.isStopped = false;
        waypoints = new List<GameObject>();
        waypoints = npc.GetComponent<Enemy_AI>().ArrayPoints;
        NeedTocheck = npc.GetComponent<Enemy_AI>().IsRandomPatroling;
        ai = npc.GetComponent<Enemy_AI>();

    }

    public override void Enter()
    {
        currentIndex = 0;
       
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log(agent.remainingDistance);

        if (CanSeePlayer())
        {
            nextState = new Chase(npc, agent, anim, player, npcHealth);
            stage = EVENT.EXIT;
        }
        else if (agent.remainingDistance < 1f && !ai.IsLooting)
        {

            if (NeedTocheck && (Random.Range(0.0f, 1.0f) < 0.5) && FirtPoint)
            {
                ai.IsLooting = true;
                agent.isStopped = true;
                SetLooting();
                return;
            }

            if (currentIndex >= waypoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }



            agent.SetDestination(waypoints[currentIndex].transform.position);
            FirtPoint = true;
            agent.isStopped = false;
        }

    }

    public override void Exit()
    {
        ai.IsLooting = false;
        base.Exit();
    }

    void SetLooting()
    {
        Vector3 lootingPost = waypoints[currentIndex].transform.position -npc.transform.position;
        lootingPost.y = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(lootingPost), Time.deltaTime * 2.0f);
        if (waypoints[currentIndex].transform.position.y < 0.5f)
        {
            anim.SetTrigger("LootingLow");
        }
        else
        {
            anim.SetTrigger("LootingUp");

        }
    }


}
