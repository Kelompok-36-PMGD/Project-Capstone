using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //public AudioClip coinSound;
    Transform player;
    Rigidbody2D rb;
    bool followingPlayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        //Launch the coin upward in random cone direction
        rb.AddForce(new Vector2(Random.Range(-2f, 2f), 5f), ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        if (followingPlayer)
        {
            float movementX = Mathf.Sign(rb.position.x - player.position.x);
            float movementY = Mathf.Sign(rb.position.y - player.position.y);
            rb.MovePosition(rb.position - new Vector2(movementX, movementY) * 5 * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !followingPlayer)
        {
            player = collision.transform;
            followingPlayer = true;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag == "Player" && followingPlayer)
        {
            GameManager.instance.coinScriptable.value += 1;
            gameObject.SetActive(false);
        }
        
    }

    public void SetDelayPick()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("DelayPick", 2f);
    }

    void DelayPick()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
