using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    private Vector3 spawnLoc;
    private Rigidbody playerRB;

    public static event Action<int> OnHealthChanged;

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
    public void setYVelocity(float y){
        Vector3 v = playerRB.velocity;
        v.y=y;
        playerRB.velocity=v;
    }

    public void pushPlayer(float amount, Vector3 dir, float limit=0f){
        //playerRB.velocity = Vector3.up * amount;
        playerRB.AddForce(dir * amount, ForceMode.Acceleration);
        if(limit>0&&playerRB.velocity.magnitude>limit){
            playerRB.velocity = playerRB.velocity.normalized * limit;
        }
    }
    public static int PlayerHealth { get; set; }

    public void damagePlayer(int damage)
    {
        PlayerHealth -= damage;
        if(PlayerHealth <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }

        OnHealthChanged?.Invoke(PlayerHealth);
    }
}
