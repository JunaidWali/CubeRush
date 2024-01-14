using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play("MainMenuTheme");
    }

    public void OnSinglePlayerButtonClick()
    {
        AudioManager.Instance.Stop("MainMenuTheme");
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.SinglePlayer);
        GameManager.Instance.StartGameAs();
    }

    public void OnMultiPlayerButtonClick()
    {
        AudioManager.Instance.Stop("MainMenuTheme");
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.MultiPlayer);
        GameManager.Instance.StartGameAs();
    }
}
