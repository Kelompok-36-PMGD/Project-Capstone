using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnterTrigger : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    public bool justOnce = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnTriggerEnter.Invoke();
            if (justOnce) Destroy(gameObject);
        }
    }
}
