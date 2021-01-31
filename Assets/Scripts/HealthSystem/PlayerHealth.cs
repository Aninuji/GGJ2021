using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public AudioSource playAudio;
    public AudioClip damageClip;
    public AudioClip deadClip;
    private bool auxiliar = false;

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

        if (auxiliar)
        {
            playAudio.PlayOneShot(damageClip, 0.4f);
        }
        else
        {
            StartCoroutine(WaitForSound(0.5f));
        }
        auxiliar = true;
    }

    public void Death()
    {
        playAudio.PlayOneShot(deadClip, 0.7f);
        StartCoroutine(WaitForSound(1.2f));
        Destroy(this.gameObject, 0.5f);


        GameManager.Instance.GameOver();
    }
    public IEnumerator WaitForSound(float time)
    {
        yield return new WaitForSeconds(time);
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
