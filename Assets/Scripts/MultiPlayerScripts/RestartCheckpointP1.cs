using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCheckpointP1 : MonoBehaviour
{

    public Player1Movement movement;     // A reference to our PlayerMovement script
    public Transform player;             // A reference to the player's transform
    public float restartDelay = 1f;         // Time to wait before restarting the level

    public void RestartFromCheckpoint()
    {
        if (gameObject.name == "Player1")
        {
            // We check if the object we collided with has a tag called "Obstacle".
            Invoke("Restart", restartDelay);
        }

        else if (gameObject.name == "Player2")
        {
            // We check if the object we collided with has a tag called "Obstacle".
            Invoke("Restart", restartDelay);
        }
    }

    // This function should restart the position of the player back to the starting point
    public void Restart()
    {
        if (gameObject.name == "Player1")
        {
            // Reset the position of the player
            movement.enabled = true;
            player.position = new Vector3(0, 1, 0);
            player.rotation = Quaternion.identity;
        }

        else if (gameObject.name == "Player2")
        {
            // Reset the position of the player
            movement.enabled = true;
            player.position = new Vector3(0, 1, 0);
            player.rotation = Quaternion.identity;
        }
    }
}
