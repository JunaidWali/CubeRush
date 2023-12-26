using UnityEngine;

public class Player1Controls : PlayerInfo
{
	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow)) // If the player is pressing the right arrow key
		{
			rightMoveRequest = true;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) // If the player is pressing the left arrow key
		{
			leftMoveRequest = true;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) // If the player presses the up arrow key
		{
			if (isGrounded)
			{
				jumpRequest = true;
			}
		}
	}
}
