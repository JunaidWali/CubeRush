using UnityEngine;
using UnityEngine.SceneManagement;

public class MPLevelComplete : MonoBehaviour {

	public void LoadNextLevel ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
