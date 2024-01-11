using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode { SinglePlayer, MultiPlayer }
    public GameMode CurrentGameMode { get; private set; }

    [SerializeField] private Animator transition;
    private float transitionTime = 0.5f;

    private int activeSceneIndex;
    private string activeSceneName;

    private bool isPaused = false;

    private void Awake()
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

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void SetIsPaused(bool value)
    {
        isPaused = value;
    }

    public void StartGameAs(GameMode mode)
    {
        string animationTrigger;
        string gameModeScene;

        CurrentGameMode = mode;

        if (mode == GameMode.SinglePlayer)
        {
            animationTrigger = "Load_SP";
            gameModeScene = "SP_GameMode";
        }
        else
        {
            animationTrigger = "Load_MP";
            gameModeScene = "MP_GameMode";
        }

        transition.SetTrigger(animationTrigger);
        StartCoroutine(LoadStartUpScenes(gameModeScene, "Level01", "Environment"));
    }

    public IEnumerator LoadStartUpScenes(string gameModeScene, string levelName, string environmentName)
    {
        yield return new WaitForSeconds(transitionTime);

        // Unload and then load level
        if (SceneManager.GetSceneByName(levelName).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(levelName);
        }
        yield return StartCoroutine(LoadLevel(levelName));

        // Unload Main-Menu if loaded
        if (SceneManager.GetSceneByName("Main_Menu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Main_Menu");
        }

        // Unload and then load environment
        if (SceneManager.GetSceneByName(environmentName).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(environmentName);
        }
        SceneManager.LoadScene(environmentName, LoadSceneMode.Additive);

        // Unload and then load game mode
        if (SceneManager.GetSceneByName(gameModeScene).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(gameModeScene);
        }
        SceneManager.LoadScene(gameModeScene, LoadSceneMode.Additive);
        transition.SetTrigger("Start_Level");
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = activeSceneIndex + 1;
        string nextSceneName = GetSceneName(nextSceneIndex);
        if (nextSceneName.StartsWith("Level"))
        {
            StartCoroutine(LoadLevel(nextSceneName));
        }
        else
        {
            LoadGameOver();
        }
    }

    public IEnumerator LoadLevel(string levelName)
    {
        if (activeSceneName != null)
        {
            Scene activeScene = SceneManager.GetSceneByName(activeSceneName);
            if (activeScene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(activeSceneName);
            }
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

    public void LoadMainMenu()
    {
        ResumeGame();
        StartCoroutine(LoadScenesWithTransition("Main_Menu", "Start_MainMenu_Load", "Load_MainMenu"));
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game_Over");
    }

    public void PauseGame()
    {
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
            SceneManager.UnloadSceneAsync("Pause_Menu");
            Time.timeScale = 1f;
            isPaused = false;
        }
    }


    public void RestartLevel()
    {
        ResumeGame();
        if (SceneManager.GetSceneByName(activeSceneName).isLoaded)
        {
            transition.SetTrigger("Load_Restart");
            if (CurrentGameMode == GameMode.SinglePlayer)
            {
                StartCoroutine(LoadStartUpScenes("SP_GameMode", activeSceneName, "Environment"));
            }
            else
            {
                StartCoroutine(LoadStartUpScenes("MP_GameMode", activeSceneName, "Environment"));
            }
        }

    }

    public IEnumerator LoadScenesWithTransition(string sceneName, string startAnimation, string endAnimation)
    {
        transition.SetTrigger(startAnimation);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger(endAnimation);
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
