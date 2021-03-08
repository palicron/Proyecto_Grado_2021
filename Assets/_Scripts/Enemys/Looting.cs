using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Looting : State
{
    Vector3 lootingPost;
    float minLootTime;
    float Time = 0;

    public Looting(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, healthsystems _heal,GameObject _place) : base(_npc, _agent, _anim, _player, _heal)
    {
        name = STATE.LOOTING;

        lootingPost = _place.transform.position;
        lootingPost.x = 0;
        lootingPost.z = 0;
    }

    public override void Enter()
    {
        Vector3 pp = npc.transform.position;
        pp.x = 0;
        pp.z = 0;
     
        if (Vector3.Distance(lootingPost,pp)<0.5f)
        {
            
            minLootTime = 430;
            anim.SetTrigger("LootingLow");
        }
        else
        {
            anim.SetTrigger("LootingUp");
            minLootTime = 100;
        }

        base.Enter();
    }

    public override void Exit()
    {
        anim.ResetTrigger("LootingUp");
        anim.ResetTrigger("LootingLow");
        base.Exit();
    }

    public override void Update()
    {
        if(Time >= minLootTime)
        {
            nextState = new Idle(npc, agent, anim, player, npcHealth);
            stage = EVENT.EXIT;
            anim.ResetTrigger("LootingUp");
            anim.ResetTrigger("LootingLow");
        }
        else
        {
            Time++;
        }
        
    }

}
