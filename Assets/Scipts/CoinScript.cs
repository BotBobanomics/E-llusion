using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int coinID;
    int bitID;
    int coinFlags;
    int coins;
    float intitalY;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager =GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start(){
        //use coin flags as a bit vector
        coinFlags = PlayerPrefs.GetInt("CoinFlags",0);
        //get specific bit location for coin
        bitID = 1<<coinID;
        if(checkFlag()){
            gameObject.SetActive(false);
        }
        intitalY = transform.position.y;
    }
    void setFlag(){ //set the coins flag
        coinFlags |= bitID;
        PlayerPrefs.SetInt("CoinFlags",coinFlags);
        coins = PlayerPrefs.GetInt("Coins",0);
        coins += 1;
        PlayerPrefs.SetInt("Coins",coins);
        coinText.text = "Coins: " + coins;
    }
    bool checkFlag(){ //check if coin flag has been set
        return (coinFlags&bitID) != 0;
    }
    void OnTriggerEnter(Collider other){ //coin is collected
        if(other.CompareTag("Player")){
            audioManager.PlaySFX(audioManager.coinPickup);
            setFlag();
            gameObject.SetActive(false);
        }
    }
    void Update(){
        transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, intitalY + (Mathf.Sin(Time.time * 2) * 0.2f), transform.position.z);
    }
}
