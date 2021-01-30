using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int CurrentHealth { get; set; }

    void TakeDamage(int Damage);
    void HealDamage(int Heal);
    void Death();
}
