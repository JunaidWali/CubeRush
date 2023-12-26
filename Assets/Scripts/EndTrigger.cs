using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	void OnTriggerEnter(Collider player)
	{
		GameManager.LoadNextLevel();
	}
}
