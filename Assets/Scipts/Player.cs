using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    public int maxHealth; // Maximum health 
    public int currentHealth; // Current health 

    private void Awake()
    {
        // record the scene number on load
        PlayerManager.PlayerHealth = maxHealth;
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
    }
}
