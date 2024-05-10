using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private AudioSource buttonClickAudio;
    private AudioSource mainMenuThemeAudio;

    void Start()
    {
        buttonClickAudio = AudioManager.Instance.GetSource("ButtonClick");
        mainMenuThemeAudio = AudioManager.Instance.GetSource("MainMenuTheme");
        mainMenuThemeAudio.Play();
    }

    public void OnSinglePlayerButtonClick()
    {
        mainMenuThemeAudio.Stop();
        buttonClickAudio.Play();
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.SinglePlayer);
        GameManager.Instance.StartGameAs();
    }

    public void OnMultiPlayerButtonClick()
    {
        mainMenuThemeAudio.Stop();
        buttonClickAudio.Play();
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.MultiPlayer);
        GameManager.Instance.StartGameAs();
    }
}
