using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class talking : State
{

    NPC Ai;
    int Index;
    public talking(GameObject _npc, NavMeshAgent _agent, Animator _anim,int _CurrentIndex) : base(_npc, _agent, _anim, null, null)
    {
        name = STATE.TALKING;  
        Ai = _npc.GetComponent<NPC>();
        Index = _CurrentIndex;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        base.Enter();
    }

    public override void Exit()
    {
        agent.isStopped = false;
        base.Exit();
    }

    public override void Update()
    {

        
        if(!Ai.IsTalking())
        {
            nextState = new Patrol_Wanderer(npc, agent, anim, Index);
            stage = EVENT.EXIT;
        }

    
    }
}
