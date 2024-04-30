using UnityEngine;

public class PlayerController : PlayerManager
{
	// Player control variables
	[SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow; // Default key for moving right
	[SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;   // Default key for moving left
	[SerializeField] private KeyCode jumpKey = KeyCode.UpArrow;         // Default key for jumping

	void Update()
	{
		if (!pauseMenu.isGamePaused)
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
	}
}
