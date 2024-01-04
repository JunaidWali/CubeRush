using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider playerObject)
    {
        PlayerController player = playerObject.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.SetRespawnPos(transform.position);
        }
    }
}
