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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player_Arrow" || collision.gameObject.tag == "Player_Melee")
        {
            if (collision.gameObject.tag == "Player_Arrow")
            {
                Destroy(collision.gameObject);
            }
            TakeDamage(1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Arrow" || other.gameObject.tag == "Player_Melee")
        {
            TakeDamage(5);
        }

    }

}
