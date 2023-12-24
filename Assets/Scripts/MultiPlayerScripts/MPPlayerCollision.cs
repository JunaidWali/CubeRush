using UnityEngine;

public class MPPlayerCollision : MonoBehaviour
{

    public MPPlayerMovement movement;     // A reference to our PlayerMovement script

    // This function runs when we hit another object.
    // We get information about the collision and call it "collisionInfo".
    void OnCollisionEnter(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Obstacle".
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;   // Disable the players movement.
            FindObjectOfType<MPRestartCheckpoint>().Restart();
        }

        // We check if the object we collided with has a tag called "Ground".
        if (collisionInfo.collider.tag == "Ground")
        {
            movement.setGrounded(true); // Enable the player1's movement.
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Ground".
        if (collisionInfo.collider.tag == "Ground")
        {
            movement.setGrounded(false); // Enable the player1's movement.
        }
    }

}
