using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3f;

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
            Debug.Log("Player detected!");
        }
    }
}