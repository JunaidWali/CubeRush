using UnityEngine;

public class CheckpointP2 : MonoBehaviour
{
    public Player2Movement movement;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player2")
        {
            movement.setRespawnPos(transform.position);
        }
    }
}
