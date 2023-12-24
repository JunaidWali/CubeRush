using UnityEngine;
using UnityEngine.SceneManagement;

public class MPGameManager : MonoBehaviour
{

	bool gameHasEnded = false;

	public float restartDelay = 1f;

	public void CompleteLevel()
	{

		SceneManager.LoadScene("MP_Level01");

	}

	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
