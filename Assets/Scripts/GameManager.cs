using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameMode { SinglePlayer, MultiPlayer }
    public static GameMode CurrentGameMode { get; private set; }

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
        string prefix = CurrentGameMode == GameMode.SinglePlayer ? "SP" : "MP";

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName.StartsWith(prefix))
            {
                LoadLevel(sceneName);
                break;
            }
        }
    }



    public static void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
        string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);

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

    public static void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("Main_Menu");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("Game_Over");
    }

    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        SceneManager.LoadScene("Lighting", LoadSceneMode.Additive);
    }

    public static void PauseGame()
    {
        // Load the pause menu scene additively
        SceneManager.LoadScene("Pause_Menu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
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
        ResumeGame();
        string activeSceneName = SceneManager.GetActiveScene().name;
        LoadLevel(activeSceneName);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
