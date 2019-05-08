using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackAbility : MonoBehaviour, IAttack
{
    public GameObject FirePoint;
    public GameObject BulletPref;
    //public FloatVariable MaxDamage;
    public FloatVariable MaxAttackRange;
    public FloatVariable Cooldown;

    public delegate void MethodContainer();
    public event MethodContainer onTargetDie;

    //float damage;
    float attackRange;
    TargetDetectAbility targetDetector;
    GameObject target;
    HealthAbility enemyHealth;



    private void Start()
    {

        //damage = MaxDamage.value;
        attackRange = MaxAttackRange.value;
        targetDetector = transform.GetComponent<TargetDetectAbility>();

    }

    public void AimTarget()
    {
        target = targetDetector.GetTarget();
        if (target == null)
            onTargetDie?.Invoke();
        else
        {
            enemyHealth = target.GetComponent<HealthAbility>();
            //transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position), transform.up);
            //FirePoint.transform.rotation = transform.rotation;
            Vector3 point = target.transform.position;
            point.y = 0;
            //point.z = 0;
            transform.LookAt(point);
            
            //FirePoint.transform.LookAt(target.transform);
        }

    }
    float timer;

    public void Attack()
    {
        timer += Time.deltaTime;
       // Debug.Log("Timer: " + timer);
        if (enemyHealth != null && timer > Cooldown.value)
        {
            Instantiate(BulletPref, FirePoint.transform.position, transform.rotation);
            timer = 0;
        }    
        else
            onTargetDie?.Invoke();
        
    }

   
}
