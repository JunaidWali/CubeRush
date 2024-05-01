using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
	[SerializeField] private Animation endTransition;
	[SerializeField] private Camera endCamera;

	private bool hasTriggered = false;
	public string playerName;

	void OnTriggerEnter(Collider player)
	{
		if (!hasTriggered)
		{
			GameObject.Find("EventSystem").SetActive(false);
			hasTriggered = true;
			playerName = player.GetComponentInChildren<PlayerManager>().playerName;
			endCamera.enabled = true;
			endTransition.Play("LevelComplete");
			LoadLevelComplete();
		}
	}

	public void LoadLevelComplete()
    {
        SceneManager.LoadSceneAsync(GameManager.UIScene.UI_LevelComplete.ToString(), LoadSceneMode.Additive);
    }
}
