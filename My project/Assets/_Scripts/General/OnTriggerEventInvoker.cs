using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnTriggerEventInvoker : MonoBehaviour
{
    bool invoked;
    public UnityEvent customEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invoked)
        {
            customEvent.Invoke();
            invoked = true;
        }
       
        
    }
}
