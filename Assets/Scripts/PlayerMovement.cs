using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    void Start()
    {
        float distance = 15f;

        if (distance < 10f)
        {
            Debug.Log("Player detected!");
        }
        else
        {
            Debug.Log("Player is too far away.");
        }
    }

}