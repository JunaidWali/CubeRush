using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenuButton()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void QuitGameButton()
    {
        GameManager.Instance.QuitGame();
    }
}
