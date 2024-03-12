using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public Slider VolumeSlider;
    public Slider SensSlider;

    private void Start()
    {
        SensSlider.value = PlayerPrefs.GetFloat("Sensitivity",1f);
    }
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        Debug.Log(PlayerPrefs.GetFloat("Sensitivity"));
    }
    public void Reset(){
        PlayerPrefs.SetFloat("Sensitivity", 1f);
        SensSlider.value = 1f;
    }
}
