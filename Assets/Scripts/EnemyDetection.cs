using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3f;

    private bool playerDetected = false;

    void Update()
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
                Debug.Log("Player detected!");
                playerDetected = true;
            }
        }
        else
        {
            if (playerDetected == true)
            {
                Debug.Log("Player lost!");
                playerDetected = false;
            }
        }
    }
}