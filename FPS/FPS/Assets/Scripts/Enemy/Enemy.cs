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
    private int currentPatrolPoint = -1;
    
    private bool completedRotation = false;
    
    [SerializeField] private float rotationSearchVelocity;

    [SerializeField] private float minChasingDistance;
    [SerializeField] private float maxChasingDistance;

    [SerializeField] private float shootMaxDistance;
    [SerializeField] private float timeBetweenShoots;

    [SerializeField] private float hearPlayerMaxDistance;
    
    [SerializeField] private float maxSeeDistance;
    [SerializeField] private LayerMask collisionLayerMask; 
    [SerializeField] private float coneAngle;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPosition;


    [SerializeField] private float deadFadeDuration;

    private Health health;
    private float startRotationPosition;


    private void Start()
    {
        navMesh = GetComponent<MoveObjectNavMesh>();
        health = GetComponent<Health>();
        
        SetState(startState);
    }

    private void Update()
    {
        PerformState();
        if (!health.IsDead())
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

        Debug.Log(gameObject.name + " is entering state: " + newState.ToString());

        // Exiting events
        switch (currentState)
        {
            case State.Alert:
                navMesh.ResumeMoving();
                break;
        }
        
        // Entering events
        switch (newState)
        {
            case State.Alert:
                navMesh.StopMoving();
                completedRotation = false;
                
                startRotationPosition = transform.eulerAngles.y;
                if (startRotationPosition < 0)
                    startRotationPosition = 360 + startRotationPosition; 
                
                break;
            
            case State.Hit:
                previousStateToHit = currentState;
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
                if (currentPatrolPoint < 0)
                {
                    currentPatrolPoint = RotateIndex(currentPatrolPoint, patrolPoints.Length, -currentPatrolPoint);
                    navMesh.GoTo(patrolPoints[currentPatrolPoint].position);
                }
                if (navMesh.IsAtPos(patrolPoints[currentPatrolPoint].position) ) {
                    currentPatrolPoint = RotateIndex(currentPatrolPoint, patrolPoints.Length, 1);
                    navMesh.GoTo(patrolPoints[currentPatrolPoint].position);
                }
                break;

            case State.Alert:
                if (!CanSeePlayer())
                    completedRotation = SearchPlayer(Time.deltaTime);
                Debug.Log("Completed rotation? " + completedRotation);
                break;
            
            case State.Chase:
                Chase(GetChasingPosition(GameManager.Instance.player.transform.position));
                break;
            
            case State.Attack:
                ShootTo(GameManager.Instance.player.transform.position, Time.deltaTime);
                break;

            case State.Die:
                PlayDeathAnimation();
                break;
            
        }
    }

    private void Chase(Vector3 chasePos)
    {
        float disX = Mathf.Abs(navMesh.currentDestination.x - chasePos.x);
        float disY = Mathf.Abs(navMesh.currentDestination.z - chasePos.z);
        
        if (new Vector2(disX, disY).magnitude < 1.5f) return;
        
        navMesh.GoTo(chasePos);
        //navMesh.GoTo(Vector3.Lerp(chasePos, transform.position, 0.5f));
        
        Debug.Log("GOING TO " + chasePos + ". CURRENT DESTINATION " + navMesh.currentDestination);
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
                if (CanHearPlayerNew())
                    SetState(State.Alert);
                break;
            
            case State.Alert:
                if (completedRotation)
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
                if (DistanceToPlayer() <= minChasingDistance || navMesh.IsAtDestination())
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

    public void Kill()
    {
        SetState(State.Die);
    }

    private bool couldHearPlayerPreviously = false;
    private bool CanHearPlayerNew()
    {
        if (couldHearPlayerPreviously)
        {
            couldHearPlayerPreviously = CanHearPlayer();
            return false;
        }
        else
        {
            couldHearPlayerPreviously =CanHearPlayer();
            return couldHearPlayerPreviously;
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

    private float previousFrameHealth;
    private bool HasBeenHit()
    {
        if (health == null)
            Debug.LogWarning("HEALTH IS NULL");
        
        bool returnValue = previousFrameHealth > health.GetHp();
        previousFrameHealth = health.GetHp();
        return returnValue;
    }
    
    private bool CanSeePlayer()
    {
        if (DistanceToPlayer() > maxSeeDistance) return false;
        
        Vector3 l_Direction = (GameManager.Instance.player.transform.position+Vector3.up*0.9f )-transform.position;
        Ray l_Ray=new Ray(transform.position, l_Direction);
        float l_Distance=l_Direction.magnitude;
        l_Direction/=l_Distance;
        bool l_Collides=Physics.Raycast(l_Ray, l_Distance, collisionLayerMask.value);
        float l_DotAngle=Vector3.Dot(l_Direction, transform.forward);
        Debug.DrawRay(transform.position, l_Direction*l_Distance, l_Collides ? Color.red : Color.yellow);
        return !l_Collides && l_DotAngle>Mathf.Cos(coneAngle*0.5f*Mathf.Deg2Rad);
    }



    private bool SearchPlayer(float deltaTime)
    {
        Vector3 eulerAngles = transform.eulerAngles;
        
        float newRotation = eulerAngles.y + rotationSearchVelocity * deltaTime;
        
        bool returnValue = eulerAngles.y < startRotationPosition && newRotation >= startRotationPosition;
        
        transform.eulerAngles = new Vector3(eulerAngles.x, newRotation, eulerAngles.z);
        
        //Debug.Log("NEW ROTATION: " + newRotation + ", Start: " + startRotationPosition ) ;

        return returnValue;
    }

    private void PlayDeathAnimation()
    {
        StartCoroutine(FadeTo(GetComponent<Renderer>().material, 0f, deadFadeDuration));
    }
    IEnumerator FadeTo(Material material, float targetOpacity, float duration) {

        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while(t < duration) {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);
            material.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
    }

    private float timeForNextShoot;
    private void ShootTo(Vector3 transformPosition, float deltaTIme)
    {
        if (timeForNextShoot <= 0)
        {
            Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity).GetComponent<Bullet>().SetTarget(GameManager.Instance.player.transform.position + Vector3.up*0.5f);
            timeForNextShoot = timeBetweenShoots;
        }
        else
        {
            timeForNextShoot -= deltaTIme;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0.5f, 1, 0.4f);
        Gizmos.DrawSphere(transform.position, maxChasingDistance);
        
        Gizmos.color = new Color(1f, 1f, 0f, 0.4f);
        Gizmos.DrawSphere(transform.position, hearPlayerMaxDistance);
        
        Gizmos.color = new Color(1f, 0f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, maxSeeDistance);
    }
}
