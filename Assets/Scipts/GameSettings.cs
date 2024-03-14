using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [Header("Volume")]
    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeText;
    public Slider volumeSlider;
    public int minVolume;
    public int maxVolume;

    [Header("Sensitivity")]
    public TextMeshProUGUI sensText;
    public Slider sensSlider;
    // min and max to be set in inspector
    public float minSens;
    public float maxSens;

    private void Awake()
    {
        Application.targetFrameRate = 61;
        audioMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }
    private void Start()
    {
        // set the max and min for volume
        volumeSlider.maxValue = maxVolume;
        volumeSlider.minValue = minVolume;

        // update volume UI
        audioMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0f);
        volumeText.text = "Volume: " + (PlayerPrefs.GetFloat("Volume", 0f) * 100).ToString("0") + "%";

        // set the max and min for sens
        sensSlider.minValue = minSens;
        sensSlider.maxValue = maxSens;

        // update sens UI
        sensSlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);
        sensText.text = "Sensitivity: " + PlayerPrefs.GetFloat("Sensitivity", 1f).ToString("0.00");
    }
    public void SetVolume(float volume)
    {
        // change the volume; math is used to convert percentage (0 to 1) to decibel
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
        volumeText.text = "Volume: " + (PlayerPrefs.GetFloat("Volume", 0f) * 100).ToString("0") + "%";
    }
    public void SetSensitivity(float sensitivity)
    {
        // Set PlayerPrefs and update text
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        sensText.text = "Sensitivity: " + PlayerPrefs.GetFloat("Sensitivity", 1f).ToString("0.00");
    }
    public void Reset(){
        PlayerPrefs.SetFloat("Sensitivity", 1f);
        sensSlider.value = 1f;
        PlayerPrefs.SetFloat("Volume", 0f);
        volumeSlider.value = 1f;
    }
    public void ResetCoins(){
        PlayerPrefs.SetFloat("CoinFlags", 0f);
        PlayerPrefs.SetFloat("Coins", 0f);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
