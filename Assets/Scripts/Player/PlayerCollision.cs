using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // A reference to the player's controller script.
    private PlayerController player;

    // The particle effect that plays when we hit an obstacle.
    private ParticleSystem particleEffect;

    void Start()
    {
        // Get the player's info
        player = GetComponent<PlayerController>();

        // Get the particle effect
        particleEffect = GetComponentInChildren<ParticleSystem>();
    }

    // This function runs when we hit another object.
    // We get information about the collision and call it "collisionInfo".
    void OnCollisionEnter(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Obstacle".
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            AudioManager.Instance.Play("PlayerCrash");       // Play the crash sound effect.
            particleEffect.Play();    // Play the particle effect.
            player.HidePlayer();      // Hide the player.
            player.DisablePlayer();   // Disable the player.
            StartCoroutine(player.RestartFromCheckpoint(2f));
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
