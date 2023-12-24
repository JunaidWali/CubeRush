using UnityEngine;

public class MPEndTrigger : MonoBehaviour
{

	public MPGameManager gameManager;

	void OnTriggerEnter()
	{
		gameManager.CompleteLevel();
	}

}
