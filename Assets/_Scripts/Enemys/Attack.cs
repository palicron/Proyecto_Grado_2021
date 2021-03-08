using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Attack : State
{
    public float RotationSpeed = 2.0f;
    Enemy_AI Ai;
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, healthsystems _heal) : base(_npc, _agent, _anim, _player, _heal)
    {
        name = STATE.ATTACKING;
        Ai = npc.GetComponent<Enemy_AI>();
    }

    public override void Enter()
    {
        Ai.Attack();
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        Vector3 dir = player.position - npc.transform.position;
        float angel = Vector3.Angle(dir, npc.transform.forward);
        dir.y = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationSpeed);
        Ai.Attack();
        Debug.Log("attacking");
        if (!CanAttackPLayer())
        {
            nextState = new Chase(npc, agent, anim, player, npcHealth);
            stage = EVENT.EXIT;
        }
      
        
    }

    public override void Exit()
    {
        Ai.resetAttack();
       
        base.Exit();
    }

}
