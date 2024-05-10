using System;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [NonSerialized] private float speed;
    [NonSerialized] private readonly float minX = -4.4f;
    [NonSerialized] private readonly float maxX = 6.4f;

    [NonSerialized] private readonly float minY = 0.6f;
    [NonSerialized] private readonly float maxY = 3.35f;

    [NonSerialized] private float direction;

    [NonSerialized] private float currVector;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        if (startPosition.x > (minX + maxX) / 2) direction = -1.0f;
        else direction = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.CompareTag("Obstacle"))
        {
            currVector = transform.position.x + (Time.deltaTime * speed * direction);
            if (currVector <= minX || currVector >= maxX)
            {
                ChangeDirection();
            }
            UpdatePosition(minX, maxX, currVector, startPosition.y, startPosition.z);
        }

        else if (gameObject.CompareTag("LowBar"))
        {
            currVector = transform.position.y + (Time.deltaTime * speed * direction);
            if (currVector <= minY || currVector >= maxY)
            {
                ChangeDirection();
            }
            UpdatePosition(minY, maxY, startPosition.x, currVector, startPosition.z);
        }
    }


    void ChangeDirection()
    {
        if ((currVector <= minX && direction < 0) ||
            (currVector >= maxX && direction > 0) ||
            (currVector <= minY && direction < 0) ||
            (currVector >= maxY && direction > 0))
        {
            direction *= -1;
            speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }
    }

    void UpdatePosition(float min, float max, float x, float y, float z)
    {
        // Clamp the new position to be within the range
        currVector = Mathf.Clamp(currVector, min, max);
        // Update the position
        transform.position = new Vector3(x, y, z);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            direction *= -1;
            speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }
    }
}
