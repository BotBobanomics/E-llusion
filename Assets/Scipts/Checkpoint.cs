using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject r;
    void Start(){
        r = transform.GetChild(1).gameObject;
    }
    void Update(){
        r.transform.localPosition = new Vector3(0f,Mathf.Sin(Time.time * 2f)*0.2f,0f);
    }
    void OnTriggerEnter(Collider other){ //set the players spawn point
        if(other.CompareTag("Player")){
            PlayerManager.Instance.setPlayerSpawn(transform.position);
            Transform particles = transform.GetChild(0);
            particles.SetParent(null);
            particles.gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
        }
    }
}
