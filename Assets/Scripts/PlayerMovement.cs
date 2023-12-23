using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	// This is a reference to the Rigidbody component called "rb"
	public Rigidbody rb;

	public float forwardForce = 8000f;  // Variable that determines the forward force
	public float sidewaysForce = 120f;  // Variable that determines the sideways force

    public float jumpForce = 5f; // Variable that determines the jump force

    
	private bool jumpRequest = false;
	private bool rightMoveRequest = false;
	private bool leftMoveRequest = false;
	private bool isGrounded;

	void Update()
	{

		if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) // If the player is pressing the "d" key or right arrow key
		{
			rightMoveRequest = true;
		}

		if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) // If the player is pressing the "a" key or left arrow key
		{
			leftMoveRequest = true;
		}

		if (Input.GetKeyDown(KeyCode.Space)) // If the player presses the "space" key
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
			FindObjectOfType<GameManager>().EndGame();
		}
	}

	public void setGrounded(bool grounded)
	{
		isGrounded = grounded;
	}
}