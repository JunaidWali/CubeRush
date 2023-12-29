using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameMode { SinglePlayer, MultiPlayer }
    public static GameMode CurrentGameMode { get; private set; }

    private static int activeSceneIndex;
    private static string activeSceneName;

    private static bool isPaused = false;

    public static bool GetIsPaused()
    {
        return isPaused;
    }

    public static void SetIsPaused(bool value)
    {
        isPaused = value;
    }

    public static void SetGameMode(GameMode mode)
    {
        CurrentGameMode = mode;
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Environment");
        if (CurrentGameMode == GameMode.SinglePlayer)
        {
            LoadLevel("SP_Level01");
        }
        else if (CurrentGameMode == GameMode.MultiPlayer)
        {
            LoadLevel("MP_Level01");
        }
    }

    public static void LoadNextLevel()
    {
        int nextSceneIndex = activeSceneIndex + 1;
        string nextSceneName = getSceneName(nextSceneIndex);
        string prefix = CurrentGameMode == GameMode.SinglePlayer ? "SP" : "MP";
        if (nextSceneName.StartsWith(prefix))
        {
            LoadLevel(nextSceneName);
        }
        else
        {
            LoadGameOver();
        }
    }

    public static void LoadLevel(string levelName)
    {
        if (activeSceneName != null)
        {
            Scene activeScene = SceneManager.GetSceneByName(activeSceneName);
            if (activeScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(activeSceneName);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == levelName)
            {
                SceneManager.SetActiveScene(scene);
                activeSceneName = levelName;
                activeSceneIndex = scene.buildIndex;
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }
    }

    public static void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Main_Menu");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("Game_Over");
    }

    public static void PauseGame()
    {
        // Load the pause menu scene additively
        Time.timeScale = 0f;
        SceneManager.LoadScene("Pause_Menu", LoadSceneMode.Additive);
        isPaused = true;
    }

    public static void ResumeGame()
    {
        // Unload the pause menu scene
        if (SceneManager.GetSceneByName("Pause_Menu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Pause_Menu");
            Time.timeScale = 1f;
            isPaused = false;
        }
    }


    public static void RestartLevel()
    {
        if (SceneManager.GetSceneByName(activeSceneName).isLoaded)
        {
            LoadLevel(activeSceneName);
        }
        ResumeGame();
    }

    public static string getSceneName(int buildIndex)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
