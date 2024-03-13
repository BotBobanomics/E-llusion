using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthBarSlider;
    public TextMeshProUGUI HealthBarText;
    // Start is called before the first frame update
    void Start()
    {
        HealthBarSlider.maxValue = 100;
        Debug.Log("HealthBar: " + PlayerManager.PlayerHealth);
        HealthBarSlider.value = PlayerManager.PlayerHealth;
        HealthBarText.text = "Health: " + PlayerManager.PlayerHealth;
        PlayerManager.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDestroy()
    {
        PlayerManager.OnHealthChanged -= UpdateHealthBar;
    }

    // Update is called once per frame
    public void UpdateHealthBar(int health)
    {
        HealthBarSlider.value = health;
        HealthBarText.text = "Health: " + health.ToString();
    }
}
