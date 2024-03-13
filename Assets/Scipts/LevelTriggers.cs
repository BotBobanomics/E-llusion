using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//control different triggers in one script
public class LevelTriggers : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] bool setSpawn;
    [SerializeField] bool respawnPlayer;
    [Header("Push")]
    [SerializeField] bool pushPlayer;
    [SerializeField] float pushAmount;
    [Header("Gravity")]
    [SerializeField] bool changeGravity;
    [SerializeField] float gravAmount;

    private Coroutine effectsCoroutine;

    private void OnTriggerEnter(Collider other){
        //check if the trigger is related to spawn, else start
        if(other.CompareTag("Player")){
            //Debug.Log("Player Enter Trigger");
            if(respawnPlayer){
                GameManager.PlayerHealth -= 20;
                PlayerManager.Instance.respawnPlayer();
            }
            else if(setSpawn){
                PlayerManager.Instance.setPlayerSpawn(transform.position);
            }
            else{
                startEffect();
            }
        }
    }
    private void OnTriggerExit(Collider other){
        //check if trigger is related to spawn, else stop
        if(other.CompareTag("Player")){
            //Debug.Log("Player Exit Trigger");
            if(!respawnPlayer&&!setSpawn){
                stopEffect();
            }
        }
    }
    //continously trigger effect until player leaves trigger area
    private void startEffect(){
        effectsCoroutine = StartCoroutine(effects());
    }
    private void stopEffect(){
        StopCoroutine(effectsCoroutine);
    }
    IEnumerator effects(){
        while(true){
            if(pushPlayer){
                PlayerManager.Instance.pushPlayer(pushAmount,Vector3.up,20f);
            }
            if(changeGravity){
                PlayerManager.Instance.setYVelocity(gravAmount);
            }

            yield return null;
        }
    }
}
