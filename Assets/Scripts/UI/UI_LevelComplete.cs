using UnityEngine;

public class UI_LevelComplete : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.SetVolume("LevelTheme", 0.1f);
    }

    public void OnRestartLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", 0.3f);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", 0.3f);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.SetVolume("LevelTheme", 0.3f);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu);
    }
}
