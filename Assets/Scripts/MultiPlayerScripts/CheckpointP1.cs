using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointP1 : MonoBehaviour
{
    public Player1Movement player1Movement;

    void OnTriggerEnter()
    {

        // Call the method in Player1Movement that updates the respawn position
        player1Movement.setRespawnPos(transform.position);

    }


}
