using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float speed = 2.0f;
    private float maxDistance = 5.0f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Debug.Log("Start position: " + startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector3(Mathf.PingPong(Time.time * speed, maxDistance * 2) - maxDistance, 0, 0);
    }
}

