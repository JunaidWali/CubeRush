using UnityEngine;

public class LevelComplete : MonoBehaviour
{
	void OnTriggerEnter()
	{
		GameManager.Instance.LoadUI(GameManager.UIScene.UI_LevelComplete);
	}
}
