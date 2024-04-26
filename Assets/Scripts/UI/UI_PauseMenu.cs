using System.Collections;
using UnityEngine;

public class UI_PauseMenu : MonoBehaviour
{

    private Canvas pauseMenu;
    private UI_CountdownController countdownController;
    private bool isGamePaused = false;
    private bool isCountdownFinished = true;

    void Awake()
    {
        pauseMenu = GetComponent<Canvas>();
    }

    void Update()
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
        GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu);
    }

    public void PauseGame()
    {
        AudioManager.Instance.PauseAllExcept("LevelTheme");
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolumeSilenced);
        AudioManager.Instance.Play("Pause");
        Time.timeScale = 0f;
        pauseMenu.enabled = true;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        isCountdownFinished = false;
        countdownController = FindObjectOfType<UI_CountdownController>();
        StartCoroutine(countdownController.CountdownToStart(OnCountdownFinished));
    }

    private void OnCountdownFinished()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.UnPauseAll();
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("Unpause");
        isCountdownFinished = true; 
        isGamePaused = false;
    }

}
