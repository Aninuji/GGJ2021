using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
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


        GameManager.Instance.GameOver();
    }

    void Start()
    {
        MaxHealth = (int)UpgradeManager.Instance.Health.value;
        CurrentHealth = MaxHealth;
        bar.SetMaxHealth(MaxHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy_Arrow" || collision.gameObject.tag == "Enemy_Melee")
        {
            if (collision.gameObject.tag == "Enemy_Arrow")
            {
                Destroy(collision.gameObject);
            }
            TakeDamage(1);
        }

    }
}
