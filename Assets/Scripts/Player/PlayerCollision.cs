using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // A reference to the player's controller script.
    private PlayerManager player;

    [SerializeField] private AudioSource playerCrashAudio;
    [SerializeField] private AudioSource playerGroundHitAudio;

    // The particle effect that plays when we hit an obstacle.
    private ParticleSystem particleEffect;

    private readonly HashSet<string> obstacleTags = new() { "Obstacle", "LowBar" };

    void Start()
    {
        // Get the player's info
        player = GetComponent<PlayerManager>();

        // Get the particle effect
        particleEffect = GetComponentInChildren<ParticleSystem>();
    }

    // This function runs when we hit another object.
    // We get information about the collision and call it "collisionInfo".
    void OnCollisionEnter(Collision collisionInfo)
    {
        // We check if the object we collided with has a tag called "Obstacle" or "LowBar".
        if (obstacleTags.Contains(collisionInfo.collider.tag))
        {
            playerCrashAudio.Play();  // Play the crash sound effect.
            particleEffect.Play();    // Play the particle effect.
            player.HidePlayer();      // Hide the player.
            player.DisablePlayer();   // Disable the player.
            StartCoroutine(player.RestartFromCheckpoint(2f));
        }

        // We check if the object we collided with has a tag called "Ground".
        if (collisionInfo.collider.CompareTag("Ground"))
        {
            playerGroundHitAudio.Play();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            player.boostLevel += 20;
        }
    }
}
