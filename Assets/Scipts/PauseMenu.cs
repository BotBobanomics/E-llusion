using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject GameUI;   // Game UI from Canvas
    public GameObject PauseMenuUI;  // Pause Menu UI from Canvas

    private void Start()
    {
        // start of game make sure the right UI shows and the rest is inactive
        PauseMenuUI.SetActive(false);
        GameUI.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Clicking escape to pause/resume the game
        {
            if (!gameIsPaused)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
    }
    public void Pause()
    {
        // change the UI accordingly, then freeze the game and unlocking the cursor
        PauseMenuUI.SetActive(true);
        GameUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        // change the UI accordingly, then unfreeze the game and locking the cursor
        PauseMenuUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
