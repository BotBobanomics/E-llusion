using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    TextMeshProUGUI coinText;
    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        int coins = PlayerPrefs.GetInt("Coins",0);
        coinText.text = "Coins: " + coins;
    }
}
