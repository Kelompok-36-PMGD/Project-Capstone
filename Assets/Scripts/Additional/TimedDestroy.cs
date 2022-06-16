using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    [SerializeField]float time = 5f;

    private void OnEnable()
    {
        Invoke("Delay", time);
    }

    void Delay()
    {
        Destroy(gameObject);
    }
}
