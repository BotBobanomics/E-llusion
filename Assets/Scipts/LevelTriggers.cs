using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(other.CompareTag("Player")){
            //Debug.Log("Player Enter Trigger");
            if(respawnPlayer){
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
        if(other.CompareTag("Player")){
            //Debug.Log("Player Exit Trigger");
            if(!respawnPlayer&&!setSpawn){
                stopEffect();
            }
        }
    }
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