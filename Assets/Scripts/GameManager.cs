using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum GameMode { SinglePlayer, MultiPlayer }
	public static GameMode CurrentGameMode { get; private set; }

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
		Time.timeScale = 0f;
	}

	public static void ResumeGame()
	{
		Time.timeScale = 1f;
	}

	public static void RestartLevel()
	{
		string activeSceneName = SceneManager.GetActiveScene().name;
		LoadLevel(activeSceneName);
	}

	public static void QuitGame()
	{
		Application.Quit();
	}
}
