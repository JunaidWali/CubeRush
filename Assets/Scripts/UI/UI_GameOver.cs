using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    public void MainMenuButton()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu);
    }

    public void QuitGameButton()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.QuitGame();
    }
}
