using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
	private Animation endTransition;
	[SerializeField] private Camera endCamera;

	[NonSerialized] public string playerName;
	[NonSerialized] public int playerPlacement = 0;

	void Awake()
	{
		endTransition = endCamera.GetComponent<Animation>();
	}

	void OnTriggerEnter(Collider player)
	{
		playerPlacement++;
		if (!GameManager.Instance.isLevelCompleted && player.CompareTag("Player"))
		{
			GameManager.Instance.isLevelCompleted = true;
			GameObject.Find("EventSystem").SetActive(false);
			playerName = player.GetComponentInChildren<PlayerManager>().playerName;
			endTransition.Play("LevelComplete");
			LoadLevelComplete();
		}
	}

	public void LoadLevelComplete()
    {
        SceneManager.LoadSceneAsync(GameManager.UIScene.UI_LevelComplete.ToString(), LoadSceneMode.Additive);
    }
}
