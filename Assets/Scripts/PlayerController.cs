using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
	// This is a reference to the Rigidbody component called "rb"
	private Rigidbody rb;

	private Vector3 respawnPos;

	// Player movement variables

	public float restartDelay = 1f;         // Time to wait before restarting the level

	public float forwardForce = 6000f;  // Variable that determines the forward force
	public float sidewaysForce = 130f;  // Variable that determines the sideways force

	public float jumpForce = 3.35f; // Variable that determines the jump force

	// Player control variables
	public KeyCode moveRightKey = KeyCode.RightArrow; // Default key for moving right
	public KeyCode moveLeftKey = KeyCode.LeftArrow; // Default key for moving left
	public KeyCode jumpKey = KeyCode.UpArrow; // Default key for jumping

	// Player movement requests
	private bool jumpRequest = false;
	private bool rightMoveRequest = false;
	private bool leftMoveRequest = false;
	private bool isGrounded = true;

	void Start()
	{
		// We assign the Rigidbody component to our rb variable
		rb = GetComponent<Rigidbody>();
		respawnPos = rb.position;
	}

	void Update()
	{
		if (Input.GetKey(moveRightKey))
		{
			rightMoveRequest = true;
		}

		if (Input.GetKey(moveLeftKey))
		{
			leftMoveRequest = true;
		}

		if (Input.GetKeyDown(jumpKey))
		{
			if (isGrounded)
			{
				jumpRequest = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameManager.GetIsPaused() == true)
			{
				GameManager.ResumeGame();
			}
			else
			{
				GameManager.PauseGame();
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
			isGrounded = false;
		}

		if (rb.position.y < -1f)
		{
			this.enabled = false;
			RestartFromCheckpoint();
		}
	}

	public void SetGrounded(bool grounded)
	{
		isGrounded = grounded;
	}

	public void RestartFromCheckpoint()
	{
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

	public void SetRespawnPos(Vector3 pos)
	{
		respawnPos = pos;
	}
}
