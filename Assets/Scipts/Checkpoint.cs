using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject r;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start(){
        r = transform.GetChild(1).gameObject;
    }
    void OnCollisionEnter(Collision other){ //set the players spawn point
        if(other.gameObject.tag=="Player"){
            audioManager.PlaySFX(audioManager.checkPoint);
            PlayerManager.Instance.setPlayerSpawn(transform.position+(Vector3.up*2));
            Transform particles = transform.GetChild(0);
            particles.gameObject.GetComponent<ParticleSystem>().Play();

        }
    }
}
