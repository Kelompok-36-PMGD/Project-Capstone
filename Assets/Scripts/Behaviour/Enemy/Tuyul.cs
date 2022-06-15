using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy_Melee))]
public class Tuyul : MonoBehaviour
{
    public float runSpeed = 4;
    public bool tuyulChasePlayer = true;
    Enemy_Melee enemy_melee;
    Patrol patrol;
    Rigidbody2D rb;
    Collider2D bodyCollider;
    Animator anim;
    float walkSpeed;

    bool isChasing;
    bool pastTurn;
    bool onStun;

    private void Awake()
    {
        enemy_melee = GetComponent<Enemy_Melee>();
        patrol = GetComponent<Patrol>();
        anim = GetComponent<Animator>();
        walkSpeed = patrol.speed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(tuyulChasePlayer && isChasing)
        {
            patrol.speed = runSpeed;
            patrol.checkingTurnAround = false;
            int mask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right, 0.1f, mask);
            if (hit)
            {
                pastTurn = true;
            }
        }
    }
    public void DecreaseCoinOnHit()
    {
        if (GameManager.instance.coinScriptable.value > 0) GameManager.instance.coinScriptable.value -= 1;
        else return;
        PlayerController.instance.DropCoin();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !onStun && tuyulChasePlayer)
        {
            if (!patrol.isAttacking) {
                patrol.DelayAttack();
                isChasing = true;
                anim.SetBool("run", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("DelayChaseOff", 2f);
            anim.SetBool("run", false);
            isChasing = false;
            patrol.speed = walkSpeed;
            patrol.StopMoving();
            patrol.DelayIdleToMove(2f);
        }
    }

    public void OnHitStunTuyulRun(float time)
    {
        anim.SetBool("run", false);
        onStun = true;
        isChasing = false;
        Invoke("DelayStun", time);
    }

    void DelayChaseOff()
    {
        if (pastTurn)
        {
            patrol.Flip();
            patrol.IgnoreOneTurnAround();
        }
        patrol.checkingTurnAround = true;
    }

    void DelayStun()
    {
        onStun = false;
    }
}
