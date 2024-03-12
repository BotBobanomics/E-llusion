using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsUI;   // Settings UI from Canvas
    public GameObject GameUI;
    void Start()
    {
        GameUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
    public void Settings()
    {
        GameUI.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void SettingsExit()
    {
        GameUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
}
