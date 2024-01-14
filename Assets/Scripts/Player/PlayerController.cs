using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// This is a reference to the Rigidbody component called "rb"
	private Rigidbody rb;

	private Vector3 respawnPos;

	public string playerName;

	// Reference to the player's spot lights
	public Light spotLight;

	// Refernce to the player's camera
	public Camera playerCamera;
	private Vector3 playerCameraStartingPosition;    // Camera starting position

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

	[SerializeField] private AudioSource playerStartMovementAudio;
	[SerializeField] private AudioSource playerContinousMovementAudio;
	[SerializeField] private AudioSource playerJumpAudio;
	private float targetPitch = 1f;

	void Awake()
	{
		// We assign the Rigidbody component to our rb variable
		rb = GetComponent<Rigidbody>();
		respawnPos = rb.position;

		playerCameraStartingPosition = playerCamera.transform.position;
	}

	IEnumerator Start()
	{
		DisablePlayer();
		yield return new WaitForSeconds(1f);
		EnablePlayer();
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
				playerJumpAudio.Play();
				jumpRequest = true;
			}
		}

		if (Input.GetKeyDown(moveRightKey) || Input.GetKeyDown(moveLeftKey))
		{
			targetPitch = 1.3f;
		}

		if (Input.GetKeyUp(moveRightKey) || Input.GetKeyUp(moveLeftKey))
		{
			targetPitch = 1f;
		}

		playerContinousMovementAudio.pitch = Mathf.Lerp(playerContinousMovementAudio.pitch, targetPitch, Time.deltaTime * 10);
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

		if (rb.position.y < -3f)
		{
			DisablePlayer();
			HidePlayer();
			StartCoroutine(RestartFromCheckpoint(1f));
		}
	}

	public void SetGrounded(bool grounded)
	{
		isGrounded = grounded;
	}

	// This function should restart the position of the player back to the starting point
	public IEnumerator RestartFromCheckpoint(float restartDelay)
	{
		yield return new WaitForSeconds(restartDelay);
		// Reset player back to starting point
		rb.position = respawnPos;
		ShowPlayer();
		// Start the coroutine to reset the camera's position
		yield return StartCoroutine(MoveCamera(restartDelay));
		EnablePlayer();
	}

	public void DisablePlayer()
	{
		this.enabled = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.rotation = Quaternion.identity;
		playerContinousMovementAudio.Stop();
		playerCamera.GetComponent<FollowPlayer>().enabled = false;
	}

	public void EnablePlayer()
	{
		playerCamera.GetComponent<FollowPlayer>().enabled = true;
		playerStartMovementAudio.Play();
		playerContinousMovementAudio.Play();
		this.enabled = true;
	}

	public void HidePlayer()
	{
		GetComponent<Renderer>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;
		spotLight.enabled = false;
	}

	public void ShowPlayer()
	{
		GetComponent<Renderer>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;
		spotLight.enabled = true;
	}

	public void SetRespawnPos(Vector3 pos)
	{
		respawnPos = pos;
	}

	IEnumerator MoveCamera(float duration)
	{
		Vector3 startingPosition = playerCamera.transform.position;
		float elapsedTime = 0;

		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration; // Calculate the fraction for Lerp
			playerCamera.transform.position = Vector3.Lerp(startingPosition, playerCameraStartingPosition, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Ensure the position is set to the starting position when the duration is over
		playerCamera.transform.position = playerCameraStartingPosition;
	}
}
