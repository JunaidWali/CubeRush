using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMovement : MonoBehaviour
{
    public float minSpeed = 2.0f;
    public float maxSpeed = 5.0f;
    private float speed;
    private float minX = -4.4f;
    private float maxX = 6.4f;
    private float direction = 1.0f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(ChangeDirectionAndSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position
        float newX = transform.position.x + (Time.deltaTime * speed * direction);

        // If the new position has reached either minX or maxX, change the direction
        if (newX <= minX || newX >= maxX)
        {
            direction *= -1;
            newX = transform.position.x + (Time.deltaTime * speed * direction); // Recalculate newX with the new direction
        }

        // Clamp the new x position to be within the platform
        newX = Mathf.Clamp(newX, minX, maxX);

        // Update the position
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);
    }

    IEnumerator ChangeDirectionAndSpeed()
    {
        while (true)
        {
            // Wait for a random amount of time between 1 and 5 seconds
            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

            // Get the current scene's name
            string sceneName = SceneManager.GetActiveScene().name;
            
            // If the scene name does not contain "03", change the direction
            if (!sceneName.Contains("03"))
            {
                direction *= -1;
            }

            // Change the speed to a random value between 0.1 and 2.0
            speed = Random.Range(minSpeed, maxSpeed);
        }
    }
}