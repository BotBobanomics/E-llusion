using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject GameUI;   // Game UI from Canvas
    public GameObject PauseMenuUI;  // Pause Menu UI from Canvas
    public GameObject SettingsUI;   // Settings UI from Canvas

    private void Start()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        // start of game make sure the right UI shows and the rest is inactive; alternatively could be replace by just using Resume()
        PauseMenuUI.SetActive(false);
        SettingsUI.SetActive(false);
        GameUI.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Clicking escape to pause/resume the game
        {
            if (GameManager.Instance.State == GameManager.GameState.Pause)
            {
                // Resume if the game is already in Pause state
                Resume();
            } else
            {
                // Pause if the game is not in Pause state
                Pause();
            }
        }
    }
    public void Pause()
    {
        // change the UI accordingly, then freeze the game and unlocking the cursor
        GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
        PauseMenuUI.SetActive(true);
        GameUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        // change the UI accordingly, then unfreeze the game and locking the cursor
        GameManager.Instance.UpdateGameState(GameManager.GameState.Play);
        PauseMenuUI.SetActive(false);
        SettingsUI.SetActive(false);
        GameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Settings()
    {
        PauseMenuUI.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void SettingsExit()
    {
        PauseMenuUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
}
