using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;


public class ValueDisplay : MonoBehaviour
{
    [Tooltip("Text component displaying that value")]
    public Text display;
    [Header("Text&Value")]
    public string message;
    public FloatVariable valueRef;



    //[Header("OnValueChange")]
    //public GameEvent gameEvent;


    //private UnityEvent response;

    //private void OnEventRaised()
    //{
    //    response.AddListener(UpdateValue);
    //    response.Invoke();
    //}

    private void Update()
    {
        display.text = message + (int)valueRef.value;
    }

    //Need to be called by event
    public void UpdateValue()
    {
        display.text = message + valueRef.value;
        
    }
   
}
