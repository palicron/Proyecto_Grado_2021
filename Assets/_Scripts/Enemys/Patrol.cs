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

        if (CanSeePlayer())
        {
            nextState = new Chase(npc, agent, anim, player, npcHealth);
            stage = EVENT.EXIT;
        }
        else if (agent.remainingDistance < 1f)
        {
            //if (NeedTocheck && (ai.lastCheckPoint != waypoints[currentIndex]))
            //{
                //Debug.Log(agent.remainingDistance + "asd");
                //ai.lastCheckPoint = waypoints[currentIndex];
               // nextState = new Looting(npc, agent, anim, player, npcHealth, waypoints[currentIndex]);
              //  stage = EVENT.EXIT;
             //   return;
           // }

            if (currentIndex >= waypoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

       

            agent.SetDestination(waypoints[currentIndex].transform.position);
        }

    }

    public override void Exit()
    {
    
        base.Exit();
    }


}
