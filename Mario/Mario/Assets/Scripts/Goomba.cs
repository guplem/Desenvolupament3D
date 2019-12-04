using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Goomba : MonoBehaviour
{
    public enum State
    {
        PATROL,
        CHASE,
    }

    private State hitPreviousState;
    public State m_CurrentState;
    private PlayerManager m_GameController;
    public List<Transform> m_PatrolPositions;
    int m_CurrentPatrolPositionId = -1;
    public LayerMask m_CollisionLayerMask;
    public float m_ConeAngle = 60.0f;
    [SerializeField] float m_ChaseRange;
    [SerializeField]private NavMeshAgent m_NavMeshAgent;
    [SerializeField] private Transform raycastFrom;


    void Start()
    {
        SetState(State.PATROL);
        m_GameController = PlayerManager.Instance;
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(m_NavMeshAgent);
    }

    // Update is called once per frame
    void Update()
    {
        StatePerform();
        StateCheckTransition();
    }

    private void StateCheckTransition()
    {
        switch (m_CurrentState)
        {
            case State.PATROL:
                if (IsInRange(PlayerManager.Instance.gameObject.transform.position,
                    m_ChaseRange))
                {
                    SetState(State.CHASE);
                }

                break;
            case State.CHASE:
                if (IsInRange(PlayerManager.Instance.transform.position,
                    m_ChaseRange))
                {
                    Chase();
                }

                if (!IsInRange(PlayerManager.Instance.transform.position, m_ChaseRange))
                {
                    SetState(State.PATROL);
                }

                break;
        }
    }

    private void StatePerform()
    {
        switch (m_CurrentState)
        {
            case State.PATROL:
                if (!m_NavMeshAgent.hasPath &&
                    m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                    MoveToNextPatrolPosition();
                break;
            case State.CHASE:
                Chase();
                break;
        }
    }

    private void SetState(State newState)
    {
        m_CurrentState = newState;
        switch (m_CurrentState)
        {
            case State.PATROL:
                MoveToClosestPatrolPosition();
                break;
        }
    }

    private void MoveToClosestPatrolPosition()
    {
        m_CurrentPatrolPositionId = GetClosestPatrolPositionId();
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.SetDestination(m_PatrolPositions[m_CurrentPatrolPositionId].position);
    }

    private int GetClosestPatrolPositionId()
    {
        int closestIndex = 0;
        float closestPosition = Mathf.Infinity;
        for (int i = 0; i < m_PatrolPositions.Count; i++)
        {
            float actualDistance = (this.transform.position - m_PatrolPositions[i].position).sqrMagnitude;
            if ((actualDistance) < closestPosition)
            {
                closestPosition = actualDistance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }


    private bool IsInRange(Vector3 position, float mMinDistanceToAlert)
    {
        return Vector3.Distance(transform.position, position) <= mMinDistanceToAlert;
    }


    private void Chase()
    {
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.destination = PlayerManager.Instance.transform.position;
    }

   


    private void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolPositionId;
        if (m_CurrentPatrolPositionId >= m_PatrolPositions.Count)
            m_CurrentPatrolPositionId = 0;
        m_NavMeshAgent.SetDestination(m_PatrolPositions[m_CurrentPatrolPositionId].position);
    }
    

    public void Kill()
    {
        Destroy(gameObject);
    }
}