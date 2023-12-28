using UnityEngine;

public class PlayerControls : PlayerInfo
{
    
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

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.Space)) // If the player presses the "space", "w" or the up arrow key
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
}