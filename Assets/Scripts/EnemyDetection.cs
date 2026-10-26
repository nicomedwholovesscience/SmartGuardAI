
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Possible AI states
    public enum EnemyState
    {
        Patrol,
        Chase,
        Search
    }

    private Renderer enemyRenderer;
    
    public Transform player;
    public Transform[] patrolPoints;

    public float detectionRange = 3f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float searchSpeed = 3f;
    public float stoppingDistance = 1f;
    
    public float waitDuration = 2f;
    private float waitTimer = 0f;
    public float searchWaitDuration = 3f;
    private float searchWaitTimer = 0f;
    
    public float viewAngle = 90f;
    

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

        CheckPlayerAngle();

        // Step 1: Decide which state to use
        if (IsPlayerNearby(detectionRange) && IsPlayerWithinViewAngle())
        {
            lastKnownPosition  = player.position;
            ChangeState(EnemyState.Chase);
        }
        else if (EnemyState.Chase == currentState)
        {
            ChangeState(EnemyState.Search);
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
            
            case EnemyState.Search:
                SearchLastKnownPosition();
                break;
        }
    }
    
    void SearchLastKnownPosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            lastKnownPosition,
            searchSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(
            transform.position,
            lastKnownPosition
        );

// Wait at the last known position before resuming patrol.
        if (distance < 0.5f)
        {
            searchWaitTimer += Time.deltaTime;

            if (searchWaitTimer >= searchWaitDuration)
            {
                ChangeState(EnemyState.Patrol);
            }
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
            case EnemyState.Search:
                ChangeEnemyColor(Color.yellow);
                searchWaitTimer = 0f;
                break;
        }
        Debug.Log("Enemy state: " + currentState);
    }
    
    void ForgetPlayerPosition()
    {
        lastKnownPosition  = Vector3.zero;
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
            Vector3 lookPosition = target.position;
            lookPosition.y = transform.position.y;
            transform.LookAt(lookPosition);
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
    void CheckPlayerAngle()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        
        Debug.Log("Angle to player: " + angleToPlayer);
        
        if (angleToPlayer < 45f)
        {
            Debug.Log("Player is inside FOV");
        }
        else
        {
            Debug.Log("Player is outside FOV");
        }
       
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Vector3 forward = Vector3.ProjectOnPlane(
            transform.forward,
            Vector3.up
        ).normalized;

        Vector3 leftDirection = Quaternion.AngleAxis(
            -viewAngle / 2f,
            Vector3.up
        ) * forward;

        UnityEditor.Handles.color =
            new Color(1f, 1f, 0f, 0.3f);

        UnityEditor.Handles.DrawSolidArc(
            transform.position + Vector3.up * 0.1f,
            Vector3.up,
            leftDirection,
            viewAngle,
            detectionRange
        );
    }
#endif
    
    bool IsPlayerWithinViewAngle()
    {
        Vector3 directionToPlayer =
            Vector3.ProjectOnPlane(
                player.position - transform.position,
                Vector3.up
            );

        Vector3 forward =
            Vector3.ProjectOnPlane(
                transform.forward,
                Vector3.up
            );

        float angleToPlayer =
            Vector3.Angle(forward, directionToPlayer);

        // Your code goes here.
        return angleToPlayer <=  (viewAngle / 2f);
    }
    
    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                180f * Time.deltaTime
            );
        }
    }

}


