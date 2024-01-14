using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public float speed = 1.5f; // rotation speed, can be adjusted in the inspector
    private Vector3 targetAxis = new Vector3(191.435f, 432.646f, -434.084f);
    private Vector3 originalAxis = new Vector3(162.302f, 282.093f, -458.342f);
    private Vector3 rotateRight = Vector3.right;

    void Update()
    {
        // Rotate the light around the current axis at 'speed' degrees per second
        transform.Rotate(rotateRight, speed * Time.deltaTime);

        // If the current rotation is close to the target rotation, change the rotation axis
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetAxis)) < 1f)
        {
            transform.rotation = Quaternion.Euler(originalAxis);
        }
    }
}