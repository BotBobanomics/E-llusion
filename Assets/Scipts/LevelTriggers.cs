using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTriggers : MonoBehaviour
{
    [Header("Push")]
    [SerializeField] bool pushPlayer;
    [SerializeField] float pushAmount;
    [Header("Gravity")]
    [SerializeField] bool disableGravity;

    private Coroutine effectsCoroutine;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            //Debug.Log("Player Enter Trigger");
            startEffect();
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            //Debug.Log("Player Exit Trigger");
            stopEffect();
        }
    }
    private void startEffect(){
        if(disableGravity){
            PlayerManager.Instance.disablePlayerGravity();
        }
        
        effectsCoroutine = StartCoroutine(effects());
    }
    private void stopEffect(){
        if(disableGravity){
            PlayerManager.Instance.enablePlayerGravity();
        }
        StopCoroutine(effectsCoroutine);
    }
    IEnumerator effects(){
        while(true){
            if(pushPlayer){
                PlayerManager.Instance.pushPlayer(pushAmount);
            }

            yield return null;
        }
    }
}
