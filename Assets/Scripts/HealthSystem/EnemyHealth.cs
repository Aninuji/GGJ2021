using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int MaxHealth;
    private int _CurrentHealth;
    public int CurrentHealth
    {
        get
        {
            return _CurrentHealth;
        }
        set
        {
            _CurrentHealth = value;
            if (_CurrentHealth <= 0)
            {
                Death();
            }
        }
    }

    public HealthBar bar;


    public void HealDamage(int Heal)
    {
        CurrentHealth += Heal;
        bar.SetHealth(CurrentHealth);
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        bar.SetHealth(CurrentHealth);

    }

    public void Death()
    {
        Destroy(this.gameObject, 0.5f);
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        bar.SetMaxHealth(MaxHealth);
    }

}
