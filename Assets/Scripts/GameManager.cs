using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	bool gameHasEnded = false;

	public float restartDelay = 1f;

	public GameObject completeLevelUI;

	public void CompleteLevel()
	{
		// Get the build index of the current active scene
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		// Check if there is another scene to load
		if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
		{
			// Load the next scene
			LoadNextLevel();
		}
		else
		{
			// Load the first scene
			SceneManager.LoadScene(0);
		}
	}

	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void EndGame()
	{
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			Debug.Log("GAME OVER");
			Invoke("Restart", restartDelay);
		}
	}

	void Restart()
	{
		// Reloads the current active scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
