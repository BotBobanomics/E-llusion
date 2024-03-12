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
    void Start(){
        //use coin flags as a bit vector
        coinFlags = PlayerPrefs.GetInt("CoinFlags",0);
        //get specific bit location for coin
        bitID = 1<<coinID;
        if(checkFlag()){
            gameObject.SetActive(false);
        }
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
            setFlag();
            gameObject.SetActive(false);
        }
    }
}
