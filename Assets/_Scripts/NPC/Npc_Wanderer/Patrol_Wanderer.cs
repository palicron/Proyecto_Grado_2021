using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Patrol_Wanderer : State
{

    int currentIndex = 0;
  
    List<GameObject> waypoints;

    Npc_waderer ai;


    public Patrol_Wanderer(GameObject _npc, NavMeshAgent _agent, Animator _anim,int _currentIndex = 0) : base(_npc, _agent, _anim,null,null)
    {
        name = STATE.PATROLING;
        ai = npc.GetComponent<Npc_waderer>();
        waypoints = ai.ArrayPoints;
        currentIndex = _currentIndex;
       
    }

    public override void Enter()
    {
       
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {

        if(ai.IsTalking())
        {
            nextState = new talking(npc, agent, anim, currentIndex);
            stage = EVENT.EXIT;
        }
        else if (agent.remainingDistance < 1f )
        {

           // if (NeedTocheck && (Random.Range(0.0f, 1.0f) < 0.5) && FirtPoint)
            //{
               // ai.IsLooting = true;
               // agent.isStopped = true;
               // SetLooting();
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
   
            agent.isStopped = false;
        }

    }

  

 
}
