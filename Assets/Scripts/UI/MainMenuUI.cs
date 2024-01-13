using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void Start()
    {
        AudioManager.Instance.Play("MainMenuTheme");
    }

    public void OnSinglePlayerButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.SinglePlayer);
        GameManager.Instance.StartGameAs();
    }

    public void OnMultiPlayerButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.MultiPlayer);
        GameManager.Instance.StartGameAs();
    }
}
