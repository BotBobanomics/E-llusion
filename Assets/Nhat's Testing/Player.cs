using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    public int maxHealth; // Maximum health 
    public int currentHealth; // Current health 

    private void Start()
    {
        // Set player health to the max health at the start of the game
        GameManager.PlayerHealth = maxHealth;
    }

    private void Update()
    {
        currentHealth = GameManager.PlayerHealth;
        if (currentHealth <= 0)
        {
            Debug.Log("LOSER");
            // (handles what happens when lose)
        }
    }
}
