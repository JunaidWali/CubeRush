using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode { SinglePlayer, MultiPlayer }
    public GameMode CurrentGameMode { get; private set; }

    [SerializeField] private Animator transition;
    private readonly float transitionTime = 0.5f;

    private int activeSceneIndex;
    private string activeSceneName;

    private bool isPaused = false;
    private bool isGameActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameActive)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public GameMode GetCurrentGameMode()
    {
        return CurrentGameMode;
    }

    public void SetCurrentGameMode(GameMode mode)
    {
        CurrentGameMode = mode;
    }

    public void StartGameAs()
    {
        AudioManager.Instance.Stop("MainMenuTheme");
        string startAnimTrigger = CurrentGameMode == GameMode.SinglePlayer ? "Start_SP_Load" : "Start_MP_Load";

        StartCoroutine(LoadStartUpScenes(startAnimTrigger, "Level01", "Environment", "Load_Level"));
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = activeSceneIndex + 1;
        string nextSceneName = GetSceneName(nextSceneIndex);
        if (nextSceneName.StartsWith("Level"))
        {
            StartCoroutine(LoadStartUpScenes("Start_NextLevel_Load", nextSceneName, "Environment", "Load_Level"));
        }
        else
        {
            LoadGameOver();
        }
    }

    private IEnumerator LoadLevel(string levelName)
    {
        if (!string.IsNullOrEmpty(activeSceneName))
        {
            yield return StartCoroutine(UnloadSceneIfLoaded(activeSceneName));
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        // Wait until the scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Get the loaded scene
        Scene loadedScene = SceneManager.GetSceneByName(levelName);

        // If the scene was found, set it as the active scene
        if (loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
            activeSceneName = levelName;
            activeSceneIndex = loadedScene.buildIndex;
        }
    }

    public void LoadCompleteLevel(string playerName)
    {
        isGameActive = false;
        AudioManager.Instance.Stop("LevelTheme");
        StartCoroutine(LoadSceneWithTransition("Start_LevelComplete_Load", "Complete_Level", "Load_LevelComplete"));
    }

    public void LoadMainMenu()
    {   
        isGameActive = false;
        AudioManager.Instance.Stop("LevelTheme");
        ResumeGame();
        StartCoroutine(LoadSceneWithTransition("Start_MainMenu_Load", "Main_Menu", "Load_MainMenu"));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadSceneWithTransition("Start_NextLevel_Load", "Game_Over", "Load_GameOver"));
    }

    public void PauseGame()
    {
        AudioManager.Instance.Pause("LevelTheme");
        AudioManager.Instance.Play("Pause");
        // Load the pause menu scene additively
        Time.timeScale = 0f;
        SceneManager.LoadSceneAsync("Pause_Menu", LoadSceneMode.Additive);
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Unload the pause menu scene
        if (SceneManager.GetSceneByName("Pause_Menu").isLoaded)
        {
            AudioManager.Instance.Play("Unpause");
            SceneManager.UnloadSceneAsync("Pause_Menu");
            Time.timeScale = 1f;
            isPaused = false;
            AudioManager.Instance.UnPause("LevelTheme");
        }
    }

    public void RestartLevel()
    {
        AudioManager.Instance.Stop("LevelTheme");
        ResumeGame();
        StartCoroutine(LoadStartUpScenes("Start_Restart_Load", activeSceneName, "Environment", "Load_Level"));
    }

    private IEnumerator LoadStartUpScenes(string startAnimTrigger, string levelName, string environmentName, string endAnimTrigger)
    {
        transition.SetTrigger(startAnimTrigger);
        yield return new WaitForSeconds(transitionTime);

        // Unload if loaded and then load level
        yield return StartCoroutine(UnloadSceneIfLoaded(levelName));
        yield return StartCoroutine(LoadLevel(levelName));

        // Unload Main-Menu if loaded
        StartCoroutine(UnloadSceneIfLoaded("Main_Menu"));

        // Unload Level-Complete if loaded
        StartCoroutine(UnloadSceneIfLoaded("Complete_Level"));

        // Unload and then load environment
        yield return StartCoroutine(UnloadSceneIfLoaded(environmentName));
        yield return SceneManager.LoadSceneAsync(environmentName, LoadSceneMode.Additive);

        // Unload and then load game mode
        yield return StartCoroutine(LoadGameModeScene());

        transition.SetTrigger(endAnimTrigger);
        AudioManager.Instance.Play("LevelTheme");
        isGameActive = true;
    }

    private IEnumerator LoadGameModeScene()
    {
        string gameModeScene = CurrentGameMode == GameMode.SinglePlayer ? "SP_GameMode" : "MP_GameMode";

        yield return StartCoroutine(UnloadSceneIfLoaded(gameModeScene));
        yield return SceneManager.LoadSceneAsync(gameModeScene, LoadSceneMode.Additive);
    }

    private IEnumerator UnloadSceneIfLoaded(string SceneName)
    {
        if (SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(SceneName);
        }
    }

    public IEnumerator LoadSceneWithTransition(string startAnimTrigger, string sceneName, string endAnimTrigger)
    {
        transition.SetTrigger(startAnimTrigger);
        yield return new WaitForSeconds(transitionTime);
        yield return SceneManager.LoadSceneAsync(sceneName);
        transition.SetTrigger(endAnimTrigger);
    }

    public string GetSceneName(int buildIndex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
