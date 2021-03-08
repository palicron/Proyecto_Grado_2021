using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class State
{
    public enum STATE
    {
        IDLE, PATROLING, LOOTING, PURSUING, ATTACKING, DYING
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected healthsystems npcHealth;
    protected NavMeshAgent agent;

    float visDist = 10.0f;
    float visAngle = 30.0f;
    float attackDist = 2.0f;


    public State(GameObject _npc,NavMeshAgent _agent,Animator _anim, Transform _player, healthsystems _heal)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        npcHealth = _heal;
    }


    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        switch(stage)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                return nextState;
                
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < visDist)
        {
            float angel = Vector3.Angle(direction, npc.transform.forward);
            if(angel<visAngle)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanAttackPLayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < attackDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


