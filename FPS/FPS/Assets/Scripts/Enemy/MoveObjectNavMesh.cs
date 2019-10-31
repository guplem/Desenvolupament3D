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

    private void Awake() // Obtenir la component NavMeshAgent amb la que es treballara
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    public void GoTo(Vector3 position) // Ordena que es busqui una ruta i vagi vap al punt indicat
    {
        agent.SetDestination(position);
    }

    public bool IsAtDestination() // Indica si ha arribat a la destinació que se li ha indicat
    {
        return agent.remainingDistance < agent.radius;
    }

    public void ResumeMoving() // Ordena que es començi/torni a moure's
    {
        agent.isStopped = false;
    }
    
    public void StopMoving() // Ordena que s'aturi
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
