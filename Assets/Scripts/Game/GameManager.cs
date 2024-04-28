using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode { SinglePlayer, MultiPlayer }
    public GameMode CurrentGameMode { get; private set; }

    public enum GameModeScene
    {
        SP_GameMode,
        MP_GameMode
    }

    public enum UIScene
    {
        UI_MainMenu,
        UI_LevelComplete,
        UI_GameOver
    }

    public enum LevelScene
    {
        Level01,
        Level02,
        Level03,
        Level04
    }

    public enum EnvironmentScene
    {
        Space
    }

    [SerializeField] private Animator transition;
    private readonly float transitionTime = 0.5f;

    private int activeSceneIndex;
    private LevelScene activeSceneName;

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

    public void SetCurrentGameMode(GameMode mode)
    {
        CurrentGameMode = mode;
    }

    public void StartGameAs()
    {
        string startAnimTrigger = CurrentGameMode == GameMode.SinglePlayer ? "Start_SP_Load" : "Start_MP_Load";
        StartCoroutine(LoadStartUpScenes(startAnimTrigger, LevelScene.Level01, EnvironmentScene.Space, "Load_Level"));
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = activeSceneIndex + 1;
        string nextSceneName = GetSceneName(nextSceneIndex);
        if (Enum.TryParse(nextSceneName, out LevelScene nextLevel))
        {
            StartCoroutine(LoadStartUpScenes("Start_NextLevel_Load", nextLevel, EnvironmentScene.Space, "Load_Level"));
        }
        else
        {
            LoadUI(UIScene.UI_GameOver);
        }
    }

    private IEnumerator LoadLevel(LevelScene levelName)
    {
        if (!string.IsNullOrEmpty(activeSceneName.ToString()))
        {
            yield return StartCoroutine(UnloadSceneIfLoaded(activeSceneName.ToString()));
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName.ToString(), LoadSceneMode.Additive);

        // Wait until the scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Get the loaded scene
        Scene loadedScene = SceneManager.GetSceneByName(levelName.ToString());

        // If the scene was found, set it as the active scene
        if (loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
            activeSceneName = levelName;
            activeSceneIndex = loadedScene.buildIndex;
        }
    }

    public void LoadUI(UIScene UI_SceneName)
    {
        StartCoroutine(LoadSceneWithTransition($"Start_{UI_SceneName}_Load", UI_SceneName.ToString(), $"Load_{UI_SceneName}"));
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadStartUpScenes("Start_Restart_Load", activeSceneName, EnvironmentScene.Space, "Load_Level"));
    }

    private IEnumerator LoadStartUpScenes(string startAnimTrigger, LevelScene levelName, EnvironmentScene environmentName, string endAnimTrigger)
    {
        transition.SetTrigger(startAnimTrigger);
        yield return new WaitForSecondsRealtime(transitionTime);

        // Unload if loaded and then load environment
        yield return StartCoroutine(UnloadSceneIfLoaded(environmentName.ToString()));
        yield return SceneManager.LoadSceneAsync(environmentName.ToString(), LoadSceneMode.Additive);

        // Unload Main-Menu if loaded
        StartCoroutine(UnloadSceneIfLoaded(UIScene.UI_MainMenu.ToString()));

        // Unload Level-Complete if loaded
        StartCoroutine(UnloadSceneIfLoaded(UIScene.UI_LevelComplete.ToString()));

        // Unload and then load game mode
        yield return StartCoroutine(LoadGameModeScene());

        // Unload if loaded and then load level
        yield return StartCoroutine(UnloadSceneIfLoaded(levelName.ToString()));
        yield return StartCoroutine(LoadLevel(levelName));

        transition.SetTrigger(endAnimTrigger);
        Time.timeScale = 1f;
    }

    private IEnumerator LoadGameModeScene()
    {
        GameModeScene gameModeScene = CurrentGameMode == GameMode.SinglePlayer ? GameModeScene.SP_GameMode : GameModeScene.MP_GameMode;

        yield return StartCoroutine(UnloadSceneIfLoaded(gameModeScene.ToString()));
        yield return SceneManager.LoadSceneAsync(gameModeScene.ToString(), LoadSceneMode.Additive);
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
        yield return new WaitForSecondsRealtime(transitionTime);
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
