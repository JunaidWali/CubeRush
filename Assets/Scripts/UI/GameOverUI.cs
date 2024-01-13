using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void MainMenuButton()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadMainMenu();
    }

    public void QuitGameButton()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.QuitGame();
    }
}
