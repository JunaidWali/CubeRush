using UnityEngine;

public class LevelComplete : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		string playerName = player.GetComponent<PlayerController>().playerName;
		GameManager.Instance.LoadUI(GameManager.UIScene.UI_LevelComplete);
	}
}
