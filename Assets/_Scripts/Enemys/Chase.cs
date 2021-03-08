using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Chase : State
{

    public Chase(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, healthsystems _heal) : base(_npc, _agent, _anim, _player, _heal)
    {
        name = STATE.PURSUING;
        agent.speed = 4.5f;
        agent.isStopped = false;
 
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if(agent.hasPath)
        {
            if(CanAttackPLayer())
            {
                nextState = new Attack(npc, agent, anim, player, npcHealth);
                stage = EVENT.EXIT;
            }
            else if(!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, anim, player, npcHealth);
                stage = EVENT.EXIT;
            }
       
        }
    }

    public override void Exit()
    {

        base.Exit();
    }

}
