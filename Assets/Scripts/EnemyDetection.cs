
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Possible AI states
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    public Transform player;
    public Transform[] patrolPoints;

    public float detectionRange = 3f;
    public float moveSpeed = 2f;
    public float stoppingDistance = 1f;

    private int currentPatrolIndex = 0;

    // Our Enemy starts in Patrol
    private EnemyState currentState = EnemyState.Patrol;
    
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // Step 1: Decide which state to use
        if (IsPlayerNearby(detectionRange))
        {
            ChangeState(EnemyState.Chase);
        }
        else
        {
            ChangeState(EnemyState.Patrol);
        }

        // Step 2: Execute the current behavior
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                ChasePlayer();
                break;
        }
    }

    bool IsPlayerNearby(float range)
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        return distance < range;
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;

        Debug.Log("Enemy state: " + currentState);
    }

    void Patrol()
    {
        if (patrolPoints == null ||
            patrolPoints.Length == 0)
        {
            return;
        }

        Transform target = patrolPoints[currentPatrolIndex];

        if (target == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (HasReachedPatrolPoint(target, 0.5f)) 
        {
            currentPatrolIndex =
                (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }
    
    bool HasReachedPatrolPoint(Transform point, float arrivalDistance)
    {
        // 1. Calculate the distance to point.
        
        float distance = Vector3.Distance(
            transform.position,
            point.position
        );
        
        return distance < arrivalDistance;
    
    }

}
