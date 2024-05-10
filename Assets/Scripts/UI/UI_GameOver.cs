using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    private AudioSource buttonClickAudio;

    void Awake()
    {
        buttonClickAudio = AudioManager.Instance.GetSource("ButtonClick");
    }

    public void MainMenuButton()
    {
        AudioManager.Instance.StopAll();
        buttonClickAudio.Play();
        StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu));
    }

    public void QuitGameButton()
    {
        buttonClickAudio.Play();
        GameManager.Instance.QuitGame();
    }
}
