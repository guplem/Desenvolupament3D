using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveObjectNavMesh : MonoBehaviour
{

    private NavMeshAgent agent;
    public Vector3 currentDestination { get { return agent.destination; } }

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

    public bool IsAtDestination()
    {
       return IsAtPos(currentDestination);
    }

    public void ResumeMoving()
    {
        agent.isStopped = false;
    }
    
    public void StopMoving()
    {
        agent.isStopped = true;
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(currentDestination, 0.2f );
        } catch (Exception) { }

    }
}
