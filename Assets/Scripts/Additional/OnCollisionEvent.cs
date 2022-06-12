using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEvent : MonoBehaviour
{
    public string tags;
    public UnityEvent OnCollision;
    public bool collideWithGround = true;

    Life hitLife;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")hitLife = collision.gameObject.GetComponent<Life>();
        if (collision.gameObject.tag == tags) OnCollision.Invoke();
        if (collision.gameObject.layer == 6 && collideWithGround) Deactivate();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void DecreaseHealthOnHit(int damage)
    {
        hitLife.OnHit(damage);
    }

}
