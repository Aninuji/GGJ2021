using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private int maxHealth;

    public Gradient gradient;
    public Image fill;


    void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        maxHealth = health;
        fill.color = gradient.Evaluate(1f);
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
