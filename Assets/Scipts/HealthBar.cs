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
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarSlider.value = GameManager.PlayerHealth;
        HealthBarText.text = "Health: " + GameManager.PlayerHealth;
    }
}
