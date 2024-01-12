using UnityEngine;

public class GameOverUI : MonoBehaviour
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
