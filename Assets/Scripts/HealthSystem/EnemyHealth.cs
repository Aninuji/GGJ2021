using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Collider))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int MaxHealth;

    public GameObject deathParticles;
    public AudioSource playAudio;
    public AudioClip damageClip;
    public AudioClip deadClip;
    private bool auxiliar = false;
    private NavMeshAgent agent;
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

    public bool isAboutToDie = false;
    public HealthBar bar;

    public Collider col;

    public void HealDamage(int Heal)
    {
        CurrentHealth += Heal;
        bar.SetHealth(CurrentHealth);
    }

    public void TakeDamage(int Damage)
    {
        if (auxiliar)
        {
            playAudio.PlayOneShot(damageClip, 0.2f);
        }
        CurrentHealth -= Damage;
        bar.SetHealth(CurrentHealth);
    }

    public void Death()
    {
        playAudio.PlayOneShot(deadClip, 0.2f);
        deathParticles.SetActive(true);
        isAboutToDie = true;
        col.enabled = false;
        agent.isStopped = true;
        StartCoroutine(aboutToDie());

    }

    IEnumerator aboutToDie()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);

    }
    void Start()
    {
        CurrentHealth = MaxHealth;
        bar.SetMaxHealth(MaxHealth);
        if (!col) col = GetComponent<Collider>();
        if (!agent) agent = GetComponent<NavMeshAgent>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player_Arrow" || collision.gameObject.tag == "Player_Melee")
        {
            if (collision.gameObject.tag == "Player_Arrow")
            {
                Destroy(collision.gameObject);
                TakeDamage((int)UpgradeManager.Instance.rangeDamage.value);
            }
            else if (collision.gameObject.tag == "Player_Melee")
            {
                TakeDamage((int)UpgradeManager.Instance.meleeDamage.value);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Arrow" || other.gameObject.tag == "Player_Melee")
        {
            if (other.gameObject.tag == "Player_Arrow")
            {
                Destroy(other.gameObject);
                TakeDamage((int)UpgradeManager.Instance.rangeDamage.value);
            }
            else if (other.gameObject.tag == "Player_Melee")
            {
                TakeDamage((int)UpgradeManager.Instance.meleeDamage.value);
            }
        }

    }

}
