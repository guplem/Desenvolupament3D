using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveObjectNavMesh : MonoBehaviour
{

    private NavMeshAgent agent;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    public void GoTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public bool IsAtPos(Vector3 position)
    {
        return agent.remainingDistance < agent.radius;
    }

    public void ResumeMoving()
    {
        agent.isStopped = false;
    }
    
    public void StopMoving()
    {
        agent.isStopped = true;
    }
}
