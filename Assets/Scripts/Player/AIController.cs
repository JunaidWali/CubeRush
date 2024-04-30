using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIController : PlayerManager
{
    private enum MoveDirection
    {
        Left,
        Right,
        Forward
    }
    private MoveDirection moveDirection = MoveDirection.Forward;
    private const float XBounds = 7f;
    private const float XBoundsDanger = 6f;

    private const float RayCastDistanceStraight = 10f;
    private Vector3 directionSraight;
    private Vector3 BoxSize;

    // The angles of the raycasts.
    private readonly float[] rayCastAngles = new float[] { -20, -15, -10, -5, 5, 10, 15, 20 };
    // The direction vectors and distances for each angle.
    private Dictionary<float, Tuple<Vector3, float>> directionVectors = new();


    // public TextMeshProUGUI right;
    // public TextMeshProUGUI left;
    // public TextMeshProUGUI forward;
    // public TextMeshProUGUI jump;

    protected override void Awake()
    {
        base.Awake();
        BoxSize = transform.localScale;
        directionSraight = transform.forward;

        foreach (float angle in rayCastAngles)
        {
            // Calculate the direction of the raycasts.
            Vector3 direction = Quaternion.Euler(0, angle, 0) * directionSraight;

            // Calculate the distance for the raycasts
            float RayCastDistanceAngles = RayCastDistanceStraight / Mathf.Cos(Mathf.Deg2Rad * angle);

            directionVectors[angle] = new Tuple<Vector3, float>(direction, RayCastDistanceAngles);
        }
    }

    void Update()
    {
        if (!pauseMenu.isGamePaused)
        {
            if (!jumpRequest)
            {
                // Check if there is an obstacle in front of the player.
                if (IfObstacleForward())
                {
                    // Avoid obstacle maneuver.
                    AvoidObstacle();
                }
                else
                {
                    // There is no obstacle in front of the player, so move forward.
                    MoveForward();
                }

                StayWithinBounds();
            }
            // left.GetComponent<TextMeshProUGUI>().enabled = moveDirection == 0;
            // right.GetComponent<TextMeshProUGUI>().enabled = moveDirection == 1;
            // forward.GetComponent<TextMeshProUGUI>().enabled = moveDirection == null;
        }
    }
        

    private bool IfObstacleForward()
    {
        // Perform the Box cast.
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, BoxSize, directionSraight, out hit, Quaternion.identity, RayCastDistanceStraight))
        {
            // Check if it is an Obstacle
            if (hit.collider.CompareTag("Obstacle"))
            {
                // There was an obstacle in the direction of the box cast.
                return true;
            }
            // Check if it is a LowBar
            else if (hit.collider.CompareTag("LowBar"))
            {
                // There was a low bar in the direction of the box cast.
                StartCoroutine(Jump());
                return true;
            }
        }
        // There was no obstacle in the direction of the box cast.
        return false;
    }

    private void AvoidObstacle()
    {
        switch (moveDirection)
        {
            case MoveDirection.Left:
                // If we are moving left, keep moving left
                MoveLeft();
                break;
            case MoveDirection.Right:
                // If we are moving right, keep moving right
                MoveRight();
                break;
            default:
                // We are not moving left or right, so we need to choose a direction to move in.
                List<float> freeAngles = new List<float>();
                foreach (float angle in rayCastAngles)
                {
                    // The direction and distance of the raycast.
                    (Vector3 direction, float distance) = directionVectors[angle];

                    // Perform the Box cast.
                    if (!Physics.BoxCast(transform.position, BoxSize, direction, Quaternion.identity, distance))
                    {
                        // There was no obstacle in the direction of the box cast.
                        // Add this direction to the list of free angles.
                        freeAngles.Add(angle);
                    }
                }
                if (freeAngles.Count > 0)
                {
                    // Choose a random free angle.
                    float freeAngle = freeAngles[UnityEngine.Random.Range(0, freeAngles.Count)];
                    (freeAngle < 0 ? (Action)MoveLeft : MoveRight)();
                }
                else
                {
                    // Debug.Log("No free angles");
                }
                break;
        }
    }

    private void StayWithinBounds()
    {
        // If we are too far to the left...
        if (transform.position.x < -XBounds)
        {
            MoveRight();
        }
        // If we are too far to the right...
        else if (transform.position.x > XBounds)
        {
            MoveLeft();
        }
    }

    private void MoveForward()
    {
        float playerVelocity_X = rb.velocity.x;
        // Check if the player is moving in the x-axis.
        if (playerVelocity_X > 0)
        {
            // The player is moving right, so move left to align with the z-axis.
            MoveLeft();
        }
        else if (playerVelocity_X < 0)
        {
            // The player is moving left, so move right to align with the z-axis.
            MoveRight();
        }
        else
        {
            leftMoveRequest = false;
            rightMoveRequest = false;
        }
        moveDirection = MoveDirection.Forward;
    }

    private void MoveRight()
    {
        leftMoveRequest = false;
        rightMoveRequest = true;
        moveDirection = MoveDirection.Right;

        if (transform.position.x > XBoundsDanger && Mathf.Abs(rb.velocity.x) > 4f)
        {
            StartCoroutine(Jump());
        }
    }

    private void MoveLeft()
    {
        leftMoveRequest = true;
        rightMoveRequest = false;
        moveDirection = MoveDirection.Left;

        if (transform.position.x < -XBoundsDanger && Mathf.Abs(rb.velocity.x) > 4f)
        {
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        jumpRequest = true;
        float absVelocity = Mathf.Abs(rb.velocity.x);
        float waitTime = Mathf.Clamp(1f - absVelocity * 0.05f, 0f, 0.3f);
        yield return new WaitForSeconds(waitTime);
        jumpRequest = false;
    }


    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     foreach (float angle in rayCastAngles)
    //     {
    //         // The direction and distance of the raycast.
    //         (Vector3 direction, float distance) = directionVectors[angle];

    //         // Draw the raycast.
    //         Gizmos.DrawRay(transform.position, direction * distance);
    //     }

    //     //Draw the straight raycast
    //     Gizmos.DrawRay(transform.position, directionSraight * RayCastDistanceStraight);
    // }
}
