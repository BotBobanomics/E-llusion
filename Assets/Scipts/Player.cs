using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    public int maxHealth; // Maximum health 
    public int currentHealth; // Current health 

    private void Awake()
    {
        PlayerManager.PlayerHealth = maxHealth;
    }

    private void Update()
    {
    }
}
