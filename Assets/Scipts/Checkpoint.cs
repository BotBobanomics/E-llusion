using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other){ //set the players spawn point
        if(other.CompareTag("Player")){
            PlayerManager.Instance.setPlayerSpawn(transform.position);
        }
    }
}
