using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenuButton()
    {
        GameManager.LoadMainMenu();
    }

    public void QuitGameButton()
    {
        GameManager.QuitGame();
    }
}
