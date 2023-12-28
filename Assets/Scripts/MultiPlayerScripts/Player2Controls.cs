using UnityEngine;

public class Player2Controls : PlayerInfo
{
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.GetIsPaused())
            {
                GameManager.ResumeGame();
                GameManager.SetIsPaused(false);
            }
            else
            {
                GameManager.PauseGame();
                GameManager.SetIsPaused(true);
            }
        }
    }
}
