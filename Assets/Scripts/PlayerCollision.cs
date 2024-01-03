using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        // Get the player's info
        player = gameObject.GetComponent<PlayerController>();
    }

    // This function runs when we hit another object.
    // We get information about the collision and call it "collisionInfo".
    void OnCollisionEnter(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Obstacle".
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            player.enabled = false;   // Disable the players movement.
            player.RestartFromCheckpoint();
        }

        // We check if the object we collided with has a tag called "Ground".
        if (collisionInfo.collider.CompareTag("Ground"))
        {
            player.SetGrounded(true);
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Ground".
        if (collisionInfo.collider.CompareTag("Ground"))
        {
            player.SetGrounded(false);
        }
    }
}
