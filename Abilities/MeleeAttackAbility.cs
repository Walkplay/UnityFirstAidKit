using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAbility : MonoBehaviour, IAttack
{
    public FloatVariable MaxDamage;
    public FloatVariable MaxRange;
    public FloatVariable Cooldown;
    

    private float damage;
    private GameObject target;
    HealthAbility enemyHealth;
    TargetDetectAbility targetDetector;

    public delegate void MethodContainer();
    public event MethodContainer onTargetDie;

    public float MaxAttackRange { get; set; }
    

    void Start()
    {
        damage = MaxDamage.value;
        targetDetector = transform.GetComponent<TargetDetectAbility>();
        MaxAttackRange = MaxRange.value;
    }

    public void AimTarget()
    {
        target = targetDetector.GetTarget();
        if (target == null)
            onTargetDie?.Invoke();
        else
            enemyHealth = target.GetComponent<HealthAbility>();
        
    }

    float timer;

    public void Attack()
    {
        timer += Time.deltaTime;

        if (enemyHealth != null && timer > Cooldown.value)
        {
            enemyHealth.TakeDamage(damage);
            timer = 0;
        }
        else
            onTargetDie?.Invoke();
    }
}
