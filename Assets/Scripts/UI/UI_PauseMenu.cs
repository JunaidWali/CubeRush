using UnityEngine;

public class UI_PauseMenu : MonoBehaviour
{

    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private UI_CountdownController countdownController;
    public bool isGamePaused = false;
    public bool isCountdownFinished = false;

    void Update()
    {
        if (!GameManager.Instance.isLevelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isCountdownFinished)
            {
                if (pauseMenu.enabled && isGamePaused)
                {
                    ResumeGame();
                }
                else if (!pauseMenu.enabled && !isGamePaused)
                {
                    PauseGame();
                }
            }
        }
    }

    public void OnRestartLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.RestartLevel();
    }

    public void OnResumeButtonClick()
    {
        ResumeGame();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu));
    }

    public void PauseGame()
    {
        AudioManager.Instance.PauseAllExcept("LevelTheme");
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolumeSilenced);
        AudioManager.Instance.Play("Pause");
        pauseMenu.enabled = true;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        isCountdownFinished = false;
        StartCoroutine(countdownController.CountdownToStart(OnCountdownFinished));
    }

    private void OnCountdownFinished()
    {
        AudioManager.Instance.UnPauseAll();
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("Unpause");
        isCountdownFinished = true;
        isGamePaused = false;
    }
}
