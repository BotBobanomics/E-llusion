using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    private Vector3 spawnLoc;
    private Rigidbody playerRB;

    public static PlayerManager _instance;
    public static PlayerManager Instance {get{return _instance;}}
    private void Awake(){
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance =  this;
        }
        spawnLoc = player.transform.position;
        playerRB = player.GetComponent<Rigidbody>();
    }
    public Vector3 GetPlayerLoc(){
        return player.transform.position;
    }
    public void setPlayerSpawn(Vector3 spawnPoint){
        spawnLoc = spawnPoint;
    }
    public void respawnPlayer(){
        player.transform.position = spawnLoc;
    }
    public void disablePlayerGravity(){
        playerRB.useGravity = false;
    }
    public void enablePlayerGravity(){
        playerRB.useGravity = true;
    }
    public void pushPlayer(float amount, Vector3 dir, float limit=0f){
        //playerRB.velocity = Vector3.up * amount;
        playerRB.AddForce(dir * amount, ForceMode.Acceleration);
        if(limit>0&&playerRB.velocity.magnitude>limit){
            playerRB.velocity = playerRB.velocity.normalized * limit;
        }
    }
}
