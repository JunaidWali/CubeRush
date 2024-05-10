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
        Level04Test,
        Level05
    }

    public enum EnvironmentScene
    {
        Space
    }

    [SerializeField] private Animation sceneTransition;
    private readonly float transitionTime = 0.5f;

    private int activeSceneIndex;
    private LevelScene activeSceneName;
    public bool isLevelCompleted = false;

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
        StartCoroutine(LoadStartUpScenes(LevelScene.Level01, EnvironmentScene.Space));
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = activeSceneIndex + 1;
        string nextSceneName = GetSceneName(nextSceneIndex);
        if (Enum.TryParse(nextSceneName, out LevelScene nextLevel))
        {
            StartCoroutine(LoadStartUpScenes(nextLevel, EnvironmentScene.Space));
        }
        else
        {
            StartCoroutine(LoadUI(UIScene.UI_GameOver));
        }
    }

    private IEnumerator LoadLevel(LevelScene levelName)
    {
        if (!string.IsNullOrEmpty(activeSceneName.ToString()))
        {
            yield return StartCoroutine(UnloadSceneIfLoaded(activeSceneName));
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

    public IEnumerator LoadUI(UIScene UI_SceneName)
    {
        sceneTransition.Play("FadeIn");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadSceneAsync(UI_SceneName.ToString());
        sceneTransition.Play("FadeOut");
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadStartUpScenes(activeSceneName, EnvironmentScene.Space));
    }

    private IEnumerator LoadStartUpScenes(LevelScene levelName, EnvironmentScene environmentName)
    {
        sceneTransition.Play("FadeIn");
        yield return new WaitForSecondsRealtime(transitionTime);

        // Unload if loaded and then load environment
        yield return StartCoroutine(UnloadSceneIfLoaded(environmentName));
        yield return SceneManager.LoadSceneAsync(environmentName.ToString(), LoadSceneMode.Additive);

        // Unload Main-Menu if loaded
        StartCoroutine(UnloadSceneIfLoaded(UIScene.UI_MainMenu));

        // Unload Level-Complete if loaded
        StartCoroutine(UnloadSceneIfLoaded(UIScene.UI_LevelComplete));

        // Unload and then load game mode
        yield return StartCoroutine(LoadGameModeScene());

        // Unload if loaded and then load level
        yield return StartCoroutine(UnloadSceneIfLoaded(levelName));
        yield return StartCoroutine(LoadLevel(levelName));

        sceneTransition.Play("FadeOut");
    }

    private IEnumerator LoadGameModeScene()
    {
        GameModeScene gameModeScene = CurrentGameMode == GameMode.SinglePlayer ? GameModeScene.SP_GameMode : GameModeScene.MP_GameMode;

        yield return StartCoroutine(UnloadSceneIfLoaded(gameModeScene));
        yield return SceneManager.LoadSceneAsync(gameModeScene.ToString(), LoadSceneMode.Additive);
    }

    private IEnumerator UnloadSceneIfLoaded<T>(T SceneName) where T : Enum
    {
        if (SceneManager.GetSceneByName(SceneName.ToString()).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(SceneName.ToString());
        }
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
