using UnityEngine;

public class CheckpointP1 : MonoBehaviour
{
    public Player1Movement movement;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player1")
        {
            movement.setRespawnPos(transform.position);
        }
    }
}
