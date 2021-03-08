using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class Idle : State
{
  
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, healthsystems _heal) : base(_npc, _agent, _anim, _player, _heal)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
       
        if (npc.GetComponent<Enemy_AI>().IsPaytoling)
        {
            nextState = new Patrol(npc, agent, anim, player, npcHealth);
            stage = EVENT.EXIT;
        }
      
    }
    public override void Exit()
    {

        base.Exit();
    }
}
