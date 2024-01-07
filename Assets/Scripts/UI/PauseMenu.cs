using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void RestartLevelButton()
    {
        GameManager.RestartLevel();
    }

    public void ResumeButton()
    {
        GameManager.ResumeGame();
    }

    public void MainMenuButton()
    {
        GameManager.LoadMainMenu();
    }
}
