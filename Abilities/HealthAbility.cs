using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : MonoBehaviour
{
    public FloatVariable MaxHealth;

    private float health;

    private void Start()
    {
        health = MaxHealth.value;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }
    public float GetHealth()
    {
        return health;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
