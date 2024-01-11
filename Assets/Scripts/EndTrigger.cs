using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	void OnTriggerEnter()
	{
		Time.timeScale = 0f;
		GameManager.Instance.LoadNextLevel();
		if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SinglePlayer)
		{
			PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
			player.Reset();
		}
		else if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.MultiPlayer)
		{
			PlayerController player1 = GameObject.Find("Player 1").GetComponent<PlayerController>();
			PlayerController player2 = GameObject.Find("Player 2").GetComponent<PlayerController>();
			player1.Reset();
			player2.Reset();
		}
		Time.timeScale = 1f;
	}
}
