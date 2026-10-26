
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Possible AI states
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    private Renderer enemyRenderer;
    
    public Transform player;
    public Transform[] patrolPoints;

    public float detectionRange = 3f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float stoppingDistance = 1f;
    
    public float waitDuration = 2f;
    private float waitTimer = 0f;

    private int currentPatrolIndex = 0;
    private Vector3 lastKnownPosition;

    // Our Enemy starts in Patrol
    private EnemyState currentState = EnemyState.Patrol;
    
    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material.color = Color.green;
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
            lastKnownPosition  = player.position;
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
        
        switch (currentState)
        {
            case EnemyState.Patrol:
                ChangeEnemyColor(Color.green);
                Debug.Log("Last known position: " + lastKnownPosition);
                break;

            case EnemyState.Chase:
                ChangeEnemyColor(Color.red);
                waitTimer = 0f;
                break;
        }
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

       

        if (HasReachedPatrolPoint(target, 0.5f))
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitDuration)
            {
                currentPatrolIndex =
                    (currentPatrolIndex + 1) % patrolPoints.Length;
                
                waitTimer = 0f;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                patrolSpeed * Time.deltaTime
            );
            
            waitTimer = 0f;
        }
    }

    void ChangeEnemyColor(Color newColor)
    {
        // Write your code here.
        enemyRenderer.material.color = newColor;
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
                chaseSpeed * Time.deltaTime
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
