using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healAmount = 20;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        //Launch the coin upward in random cone direction
        rb.AddForce(new Vector2(Random.Range(-2f, 2f), 10f), ForceMode2D.Impulse);
    }

    public void ConsumePotion()
    {
        PlayerController.instance.GetComponent<Life>().OnGain(healAmount);
    }

}
