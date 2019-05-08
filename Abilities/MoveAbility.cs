using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAbility : MonoBehaviour
{
    //TODO: Animation scriptable object
    public FloatVariable Speed;
    public FloatVariable MaxRange;
    
    //public GameObjectHolder target;

    public delegate void MethodContainer();
    public event MethodContainer onTargetReach;

    
    private float speed;
    private NavMeshAgent navAgent;
    private GameObject target;
    TargetDetectAbility targetDetector;
    // Start is called before the first frame update
    void Start()
    {
        
        speed = Speed.value; 
        navAgent = transform.GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
        targetDetector = transform.GetComponent<TargetDetectAbility>();
        //angryAnt.ResetTarget();
    }
    
    public void AimTrarget()
    {
        target = targetDetector.GetTarget();
        
    }

    public void Move()
    {
        navAgent.destination = target != null ? target.transform.position : gameObject.transform.position;
        navAgent.Resume();

        //Debug.Log($"Move to {targetPoint}! Remaining dist: {(transform.position - targetPoint).sqrMagnitude}");
    }

    private void Update()
    {
        if (target == null || (transform.position - target.transform.position).sqrMagnitude <= MaxRange.value)
        {
            navAgent.Stop();
            onTargetReach?.Invoke(); 
        }
        
    }

}
