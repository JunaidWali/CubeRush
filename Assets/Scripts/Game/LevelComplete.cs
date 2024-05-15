using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
	private Animation endTransition;
	private FollowPlayer audioListener;
	[SerializeField] private Camera endCamera;

	[NonSerialized] public string playerName;
	[NonSerialized] public int playerPlacement = 0;

	void Awake()
	{
		endTransition = endCamera.GetComponent<Animation>();
		audioListener = AudioManager.Instance.GetComponent<FollowPlayer>();
	}

	void OnTriggerEnter(Collider player)
	{
		playerPlacement++;
		if (!GameManager.Instance.isLevelCompleted && player.CompareTag("Player"))
		{
			audioListener.enabled = false;
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
