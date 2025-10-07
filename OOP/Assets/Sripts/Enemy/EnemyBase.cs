using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Enemy config;

    protected NavMeshAgent agent;
    protected Transform target;

    protected enum State { Patrol, Chase, Attack, Dead }
    protected State currentState;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = config.Speed;

        target = GameObject.FindGameObjectWithTag("Player")?.transform;

        currentState = State.Patrol;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                HandlePatrol();
                break;
            case State.Chase:
                HandleChase;
                break;
            case State.Attack:
                HandleAttack;
                break;
        }

        UpdateState();
    }

    private void UpdateState()
    {
        if (target == null)
        {
            currentState = State.Patrol;
            return;
        }

        float DistanceToTarget = Vector3.Distance(transform.position, target.position);

        if (DistanceToTarget <= config.attackRange)
        {
            currentState = State.Attack;
        }

        else if (DistanceToTarget <= config.sightRange)
        {
            currentState = State.Chase;
        }
        
        else
        {
            currentState = State.Patrol;
        }

    }

}
