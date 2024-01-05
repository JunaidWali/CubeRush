using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController player;
    private ParticleSystem particleEffect;

    

    void Start()
    {
        // Get the player's info
        player = gameObject.GetComponent<PlayerController>();

        // Get the particle effect
        particleEffect = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // This function runs when we hit another object.
    // We get information about the collision and call it "collisionInfo".
    void OnCollisionEnter(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Obstacle".
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            player.enabled = false;   // Disable the players movement.
            particleEffect.Play();    // Play the particle effect.
            player.HidePlayer();      // Hide the player.
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
