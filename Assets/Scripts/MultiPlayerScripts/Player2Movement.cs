using UnityEngine;

public class Player2Movement : MonoBehaviour
{
	// This is a reference to the Rigidbody component called "rb"
	public Rigidbody rb;

	private Vector3 respawnPos = new Vector3(0, 1, 0);

	public float restartDelay = 1f;         // Time to wait before restarting the level

	public float forwardForce = 6000f;  // Variable that determines the forward force
	public float sidewaysForce = 130f;  // Variable that determines the sideways force

	public float jumpForce = 3.35f; // Variable that determines the jump force

	// Player 1 movement requests
	private bool jumpRequest = false;
	private bool rightMoveRequest = false;
	private bool leftMoveRequest = false;
	private bool isGrounded;

	void Update()
	{
		if (Input.GetKey("d")) // If the player is pressing the "d" key
		{
			rightMoveRequest = true;
		}

		if (Input.GetKey("a")) // If the player is pressing the "a" key
		{
			leftMoveRequest = true;
		}

		if (Input.GetKeyDown("w")) // If the player presses the "w" key
		{
			if (isGrounded)
			{
				jumpRequest = true;
			}
		}
	}

	// We marked this as "Fixed"Update because we
	// are using it to mess with physics.
	void FixedUpdate()
	{
		// Add a forward force
		rb.AddForce(0, 0, forwardForce * Time.deltaTime);

		if (rightMoveRequest)
		{
			// Add a force to the right
			rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			rightMoveRequest = false;
		}

		if (leftMoveRequest)
		{
			// Add a force to the left
			rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			leftMoveRequest = false;
		}

		if (isGrounded && jumpRequest)
		{
			// Add a force to jump upwards
			rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			jumpRequest = false;
		}

		if (rb.position.y < -1f)
		{
			this.enabled = false;
			RestartFromCheckpoint();
		}
	}

	public void setGrounded(bool grounded)
	{
		isGrounded = grounded;
	}

	public void RestartFromCheckpoint()
	{
		// We check if the object we collided with has a tag called "Obstacle".
		Invoke("Reset", restartDelay);
	}

	// This function should restart the position of the player back to the starting point
	public void Reset()
	{
		// Reset the position of the player back to starting point
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.rotation = Quaternion.identity;
		rb.position = respawnPos;
		this.enabled = true;
	}

	public void setRespawnPos(Vector3 pos)
	{
		respawnPos = pos;
	}
}
