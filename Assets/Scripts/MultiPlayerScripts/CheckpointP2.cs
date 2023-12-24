using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointP2 : MonoBehaviour
{
    public Player2Movement player2Movement;

    void OnTriggerEnter()
    {

        // Call the method in Player1Movement that updates the respawn position
        player2Movement.setRespawnPos(transform.position);

    }


}
