using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

[RequireComponent(typeof(MoveObjectNavMesh))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{

    enum State
    {
        Idle,
        Patrol, 
        Alert, 
        Chase,
        Attack,
        Hit,
        Die,
        NULL
    }
    private State currentState;
    private MoveObjectNavMesh navMesh;
    [SerializeField] private State startState;
    private State previousStateToHit = State.NULL;
    
    [SerializeField] private Transform[] patrolPoints;
    private int currentPatrolPoint;
    
    private float percentageRotation = 0;

    [SerializeField] private float minChasingDistance;
    [SerializeField] private float maxChasingDistance;

    [SerializeField] private float shootMaxDistance;

    [SerializeField] private float hearPlayerMaxDistance;
    
    [SerializeField] private float maxSeeDistance;

    private Health health;
    
    
    void Start()
    {
        SetState(startState);
        navMesh = GetComponent<MoveObjectNavMesh>();
        health = GetComponent<Health>();
    }
    void Update()
    {
        PerformState();
        CheckStateTransitions();
    }


    private void GoToStatePreviousToHit()
    {
        SetState(previousStateToHit);
        previousStateToHit = State.NULL;
    }
    
    private void SetState(State newState)
    {
        if (newState == currentState)
            return;

        // Entering events
        switch (newState)
        {

            case State.Hit:
                previousStateToHit = currentState;
                break;
            
            case State.Patrol:
                percentageRotation = 0f;
                break;
        }

        // Change state
        currentState = newState;
    }
    

    private void PerformState()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            
            case State.Patrol:
                if (navMesh.IsAtPos(patrolPoints[currentPatrolPoint].position)) {
                    currentPatrolPoint = RotateIndex(currentPatrolPoint, patrolPoints.Length, 1);
                    navMesh.GoTo(patrolPoints[currentPatrolPoint].position);
                }
                break;

            case State.Alert:
                if (!CanSeePlayer())
                    percentageRotation = SearchPlayer(Time.deltaTime);
                break;
            
            case State.Chase:
                navMesh.GoTo(GetChasingPosition(GameManager.Instance.player.transform.position));
                break;
            
            case State.Attack:
                ShootTo(GameManager.Instance.player.transform.position);
                break;

            case State.Die:
                PlayDeathAnimation();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private Vector3 GetChasingPosition(Vector3 transformPosition)
    {
        Vector3 playerToEnemyVectorNormalized = (transform.position - transformPosition).normalized;
        Vector3 playerToChasePosVec = new Vector3(playerToEnemyVectorNormalized.x*minChasingDistance, playerToEnemyVectorNormalized.y*minChasingDistance, playerToEnemyVectorNormalized.z*minChasingDistance);
        return transformPosition + playerToChasePosVec;
    }

    private int RotateIndex(int currentIndex, int length, int positions)
    {
        currentIndex+=positions;
        if (currentIndex >= length)
            currentIndex = (positions<0) ? (length-1) : 0 ;

        return currentIndex;
    }

    private void CheckStateTransitions()
    {
        if (health.IsDead())
        {
            SetState(State.Die);
            return;
        } 
        
        if (HasBeenHit())
        {
            SetState(State.Hit);
            return;
        }
        
        switch (currentState)
        {
            case State.Idle:
                if (CanHearPlayer())
                    SetState(State.Alert);
                break;
            
            case State.Patrol:
                if (CanHearPlayer())
                    SetState(State.Alert);
                break;
            
            case State.Alert:
                if (percentageRotation >= 100f)
                    if (previousStateToHit != State.NULL)
                        GoToStatePreviousToHit();
                    else
                        SetState(State.Patrol);
                if (CanSeePlayer())
                    SetState(State.Chase);
                break;
            
            case State.Chase:
                if (DistanceToPlayer() > maxChasingDistance)
                    SetState(State.Patrol);
                if (DistanceToPlayer() < minChasingDistance)
                    SetState(State.Attack);
                break;
            
            case State.Attack:
                if (DistanceToPlayer() > shootMaxDistance)
                    SetState(State.Chase);
                break;
            
            case State.Hit:
                SetState(State.Alert);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool CanHearPlayer()
    {
        return DistanceToPlayer() < hearPlayerMaxDistance;
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(GameManager.Instance.player.transform.position, transform.position);
    }

    public float previousFrameHealth { get; set; }
    private bool HasBeenHit()
    {
        bool returnValue = previousFrameHealth > health.GetHp();
        previousFrameHealth = health.GetHp();
        return returnValue;
    }
    
    private bool CanSeePlayer()
    {
        if (DistanceToPlayer() > maxSeeDistance) return false;
        
        if ()
        
        return true;
    }


    private float SearchPlayer(float deltaTime)
    {
        // TODO --> rotate. Returns percentage of rotation
        return 0f;
    }

    private void PlayDeathAnimation()
    {
        // TODO
    }

    private void ShootTo(Vector3 transformPosition)
    {
        Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity).GetComponent<Bullet>().SetTarget(GameManager.Instance.player.transform.position + Vector3.up*0.5f);
    }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPosition;
}
