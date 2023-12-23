using UnityEngine;
using UnityEngine.UIElements;

public class Score : MonoBehaviour {

	public Transform player;
	public Label scoreText;
	
	// Update is called once per frame
	void Update () {
		scoreText.text = player.position.z.ToString("0");
	}
}
