using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public FloatVariable timerRef;
    public GameEvent TimeOut;

    bool ticking; 

    private void FixedUpdate()
    {
       
        timerRef.value -= Time.deltaTime; //Tick tock
           
        if(timerRef.value <= 0 && ticking)
        {
            TimeOut.Raise();
            Stop();
        }
            
    }

    void Stop()
    {
        ticking = false;
    }
    
    public void SetTimer(float value)
    {
        timerRef.value = value;
        ticking = true;
    }
}
