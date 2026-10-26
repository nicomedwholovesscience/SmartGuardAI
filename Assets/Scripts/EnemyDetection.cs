using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3f;

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
        }
        else
        {
            if (playerDetected == true)
            {
                OnPlayerLost();
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