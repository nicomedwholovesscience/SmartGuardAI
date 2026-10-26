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

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance < detectionRange)
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

    void OnPlayerDetected()
    {
        Debug.Log("Player detected!");
        playerDetected = true;

        OnEnemyAlert();
    }

    void OnPlayerLost()
    {
        Debug.Log("Player lost!");
        playerDetected = false;
    }
    
    void OnEnemyAlert()
    {
        Debug.Log("Enemy is now alert!");
    }
}