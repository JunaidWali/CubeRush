using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
	// This is a reference to the Rigidbody component called "rb"
	protected Rigidbody rb;

	protected Vector3 respawnPos;

	public string playerName;

	// Reference to the player's spot lights
	protected Light spotLight;

	// Refernce to the player's camera
	protected Camera playerCamera;
	protected Vector3 playerCameraStartingPosition;    // Camera starting position

	// Player movement variables
	public float restartDelay;      // Time to wait before restarting the level
	public float forwardForce;      // Variable that determines the forward force
	public float sidewaysForce;     // Variable that determines the sideways force
	public float jumpForce;         // Variable that determines the jump force

	// Boost variables
	public float boostForce;        // Variable that determines the boost force
	public float boostLevel;  // Variable that determines the boost level
	public Slider boostMeter;    // Reference to the boost meter UI

	// Player movement requests
	protected bool jumpRequest = false;
	protected bool rightMoveRequest = false;
	protected bool leftMoveRequest = false;
	protected bool isGrounded = true;
	protected bool boostRequest = false;

	[SerializeField] protected AudioSource playerStartMovementAudio;
	[SerializeField] protected AudioSource playerContinousMovementAudio;
	[SerializeField] protected AudioSource playerJumpAudio;

	protected float targetPitch = 1f;

	protected UI_PauseMenu pauseMenu;

	private Vector3 storedVelocity;
	private Vector3 storedAngularVelocity;
	private bool justPaused = false;

	protected virtual void Awake()
	{
		// We assign the Rigidbody component to our rb variable
		rb = GetComponent<Rigidbody>();
		respawnPos = rb.position;
	}

	IEnumerator Start()
	{
		boostMeter = GetComponentInChildren<Slider>();
		boostLevel = boostMeter.value;

		// We assign the spot light component to our spotLight variable
		spotLight = transform.parent.Find("Spot Light").GetComponent<Light>();

		// We assign the camera component to our playerCamera variable
		playerCamera = transform.parent.Find("Camera").GetComponent<Camera>();
		playerCameraStartingPosition = playerCamera.transform.position;

		// We assign the pause menu component to our pauseMenu variable
		pauseMenu = FindObjectOfType<UI_PauseMenu>();

		storedAngularVelocity = Vector3.zero;
		storedVelocity = Vector3.zero;

		DisablePlayer();
		yield return new WaitForSeconds(11f);
		EnablePlayer();
	}

	// We marked this as "Fixed"Update because we
	// are using it to mess with physics.
	void FixedUpdate()
	{
		if (!pauseMenu.isGamePaused)
		{
			if (justPaused)
			{
				rb.velocity = storedVelocity;
				rb.angularVelocity = storedAngularVelocity;
				rb.freezeRotation = false;
				rb.useGravity = true;
				justPaused = false;
			}

			boostMeter.value = boostLevel;
			if (boostRequest && boostLevel > 0)
			{
				// Use boost
				rb.AddForce(0, 0, boostForce * Time.deltaTime, ForceMode.Impulse);
				boostRequest = false;

				// Decrease boost level
				boostLevel -= 0.1f; // Decrease the boost level by 0.01
			}

			// Calculate the reduction factor based on the x velocity
			float reductionFactor = 1 - (Mathf.Abs(rb.velocity.x) * 0.01f);

			// Add a forward force, scaled by the reduction factor
			rb.AddForce(0, 0, forwardForce * Time.deltaTime * reductionFactor);

			if (rightMoveRequest)
			{
				// Add a force to the right
				rb.AddForce(sidewaysForce, 0, 0, ForceMode.VelocityChange);
				rightMoveRequest = false;
			}

			if (leftMoveRequest)
			{
				// Add a force to the left
				rb.AddForce(-sidewaysForce, 0, 0, ForceMode.VelocityChange);
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
		else
		{
			if (!justPaused)
			{
				storedVelocity = rb.velocity;
				storedAngularVelocity = rb.angularVelocity;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				rb.freezeRotation = true;
				rb.useGravity = false;
				justPaused = true;
			}
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
		boostLevel = boostMeter.maxValue;
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
