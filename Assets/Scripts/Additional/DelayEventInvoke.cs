using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEventInvoke : MonoBehaviour
{
    [SerializeField]public UnityEvent DelayEvents;
    public bool valid = true;
    public void DelayInvoke(float time)
    {
        Invoke("Invoke", time);
    }   

    void Invoke()
    {
        if(valid)DelayEvents.Invoke();
    }

    public void ToggleValid()
    {
        valid = valid ? false : true;
    }
}
