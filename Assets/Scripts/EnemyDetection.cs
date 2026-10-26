
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;

    public float detectionRange = 3f;
    public float moveSpeed = 2f;
    public float stoppingDistance = 1f;

    public Transform[] patrolPoints;

    private int currentPatrolIndex = 0;
    private bool playerDetected = false;

    void Update()
    {
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        if (player == null)
        {
            return;
        }

        if (IsPlayerNearby(detectionRange))
        {
            if (playerDetected == false)
            {
                OnPlayerDetected();
            }

            ChasePlayer();
        }
        else
        {
            if (playerDetected == true)
            {
                OnPlayerLost();
            }

            Patrol();
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

        float distance = Vector3.Distance(
            transform.position,
            target.position
        );

        if (distance < 0.1f)
        {
            currentPatrolIndex =
                (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void OnPlayerDetected()
    {
        Debug.Log("Player detected!");
        playerDetected = true;
    }

    void OnPlayerLost()
    {
        Debug.Log("Player lost!");
        playerDetected = false;
    }
}
