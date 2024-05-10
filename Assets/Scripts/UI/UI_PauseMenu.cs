using UnityEngine;

public class UI_PauseMenu : MonoBehaviour
{

    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private UI_CountdownController countdownController;
    public bool isGamePaused = false;
    public bool isCountdownFinished = false;
    private AudioSource levelThemeAudio;
    private AudioSource buttonClickAudio;
    private AudioSource pauseAudio;
    private AudioSource unpauseAudio;

    void Awake()
    {
        levelThemeAudio = AudioManager.Instance.GetSource("LevelTheme");
        buttonClickAudio = AudioManager.Instance.GetSource("ButtonClick");
        pauseAudio = AudioManager.Instance.GetSource("Pause");
        unpauseAudio = AudioManager.Instance.GetSource("Unpause");
    }

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
        levelThemeAudio.volume = AudioManager.Instance.defaultVolume;
        buttonClickAudio.Play();
        GameManager.Instance.RestartLevel();
    }

    public void OnResumeButtonClick()
    {
        ResumeGame();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.StopAll();
        levelThemeAudio.volume = AudioManager.Instance.defaultVolume;
        buttonClickAudio.Play();
        StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu));
    }

    public void PauseGame()
    {
        AudioManager.Instance.PauseAllExcept("LevelTheme");
        levelThemeAudio.volume = AudioManager.Instance.silencedVolume;
        pauseAudio.Play();
        pauseMenu.enabled = true;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        isCountdownFinished = false;
        unpauseAudio.Play();
        StartCoroutine(countdownController.CountdownToStart(OnCountdownFinished));
    }

    private void OnCountdownFinished()
    {
        AudioManager.Instance.UnPauseAll();
        levelThemeAudio.volume = AudioManager.Instance.defaultVolume;
        isCountdownFinished = true;
        isGamePaused = false;
    }
}
