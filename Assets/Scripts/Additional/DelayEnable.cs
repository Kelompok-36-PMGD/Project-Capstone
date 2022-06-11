using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEnable : MonoBehaviour
{
    public void Delay(float time)
    {
        Invoke("Enable", time);
    }

    void Enable()
    {
        gameObject.SetActive(true);
    }
}
