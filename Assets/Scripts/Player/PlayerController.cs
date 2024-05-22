using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : PlayerManager
{
	// Player control variables
	[SerializeField] private InputActionAsset playerControls;

	private InputAction moveRight;
	private InputAction moveLeft;
	private InputAction jump;
	private InputAction boost;
	private InputAction brake;

	new void Awake()
	{
		base.Awake();
		// Initialize the InputActions
		moveRight = playerControls.FindAction("PlayerControls/MoveRight");
		moveLeft = playerControls.FindAction("PlayerControls/MoveLeft");
		jump = playerControls.FindAction("PlayerControls/Jump");
		boost = playerControls.FindAction("PlayerControls/Boost");
		brake = playerControls.FindAction("PlayerControls/Brake");
	}

	private void OnEnable()
	{
		// Enable the InputActions
		moveRight.Enable();
		moveLeft.Enable();
		jump.Enable();
		boost.Enable();
		brake.Enable();
	}

	private void OnDisable()
	{
		// Disable the InputActions
		moveRight.Disable();
		moveLeft.Disable();
		jump.Disable();
		boost.Disable();
		brake.Disable();
	}

	void Update()
	{
		if (!pauseMenu.isGamePaused)
		{
			if (moveRight.ReadValue<float>() > 0)
			{
				rightMoveRequest = true;
				targetPitch = 1.3f;
			}

			if (moveLeft.ReadValue<float>() > 0)
			{
				leftMoveRequest = true;
				targetPitch = 1.3f;
			}

			if (jump.ReadValue<float>() > 0)
			{
				if (isGrounded)
				{
					playerJumpAudio.Play();
					jumpRequest = true;
				}
			}
			if (boost.ReadValue<float>() > 0 && boostLevel > 0)
			{
				boostRequest = true;
			}

			if (brake.ReadValue<float>() > 0)
			{
				brakeRequest = true;
			}

			if (moveLeft.ReadValue<float>() == 0 || moveRight.ReadValue<float>() == 0)
			{
				targetPitch = 1f;
			}

			playerContinousMovementAudio.pitch = Mathf.Lerp(playerContinousMovementAudio.pitch, targetPitch, Time.deltaTime * 10);
		}
	}
}
