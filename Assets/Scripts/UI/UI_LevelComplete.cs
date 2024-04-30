using UnityEngine;

public class UI_LevelComplete : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.StopAllExcept("LevelTheme");
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolumeSilenced);
    }

    public void OnRestartLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu));
    }
}
