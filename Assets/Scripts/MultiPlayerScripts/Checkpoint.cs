using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider player)
    {
        PlayerInfo playerInfo = player.GetComponentInParent<PlayerInfo>();
        if (playerInfo != null)
        {
            playerInfo.setRespawnPos(transform.position);
        }
    }
}
