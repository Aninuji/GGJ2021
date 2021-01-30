using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TMP_Text info;

    private int maxHealth;

    void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        info.text = health + "/" + health;
        maxHealth = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        info.text = health + "/" + maxHealth;
    }
}
