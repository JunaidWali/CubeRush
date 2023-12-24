using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPRestartCheckpoint : MonoBehaviour
{

    public MPPlayerMovement movement;     // A reference to our PlayerMovement script
    public Transform player;             // A reference to the player's transform
    public float restartDelay = 1f;         // Time to wait before restarting the level

    public void RestartFromCheckpoint()
    {
        Invoke("Restart", restartDelay);
    }

    // This function should restart the position of the player back to the starting point
    public void Restart()
    {
        // Reset the position of the player
        movement.enabled = true;
        player.position = new Vector3(0, 1, 0);
        player.rotation = Quaternion.identity;

    }
}
