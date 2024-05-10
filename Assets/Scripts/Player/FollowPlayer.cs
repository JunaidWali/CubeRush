using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

	private Transform player;   // A variable that stores a reference to our Player
	public Vector3 offset;      // A variable that allows us to offset the position (x, y, z)

	void Start()
	{
		if (CompareTag("AudioManager"))
		{
			player = GameObject.FindWithTag("Player")?.transform;
		}
		else
		{
			player = transform.parent.Find("Player").transform;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (player != null)
		{
			// Set our position to the players position and offset it
			transform.position = player.position + offset;
		}
		else
		{
			Start();
		}
	}
}
