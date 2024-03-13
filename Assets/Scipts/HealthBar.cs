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
        // Set HealthBar Slider and Text
        HealthBarSlider.maxValue = 100;
        HealthBarSlider.value = PlayerManager.PlayerHealth;
        HealthBarText.text = "Health: " + PlayerManager.PlayerHealth;

        // Subscribe to event OnHealthChanged to update healthbar
        PlayerManager.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDestroy()
    {
        // Unsubscribe to event on destroy
        PlayerManager.OnHealthChanged -= UpdateHealthBar;
    }

    // Update is called once per frame
    public void UpdateHealthBar(int health)
    {
        HealthBarSlider.value = health;
        HealthBarText.text = "Health: " + health.ToString();
    }
}
