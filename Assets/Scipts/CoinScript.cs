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
        coinFlags = PlayerPrefs.GetInt("CoinFlags",0);
        bitID = 1<<coinID;
        if(checkFlag()){
            gameObject.SetActive(false);
        }
    }
    void setFlag(){
        coinFlags |= bitID;
        PlayerPrefs.SetInt("CoinFlags",coinFlags);
        coins = PlayerPrefs.GetInt("Coins",0);
        coins += 1;
        PlayerPrefs.SetInt("Coins",coins);
        coinText.text = "Coins: " + coins;
    }
    bool checkFlag(){
        return (coinFlags&bitID) != 0;
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            setFlag();
            gameObject.SetActive(false);
        }
    }
}
