using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private bool mustTurn;

    Rigidbody2D rb;
    private Animator anim;
    public Transform[] turnAround;
    public LayerMask turnAroundMask = 9;
    Collider2D bodyCollider;

    [Header("Movement")]
    [SerializeField] float speed = 10f;
    //[SerializeField] float acceleration = 4f;
    //[SerializeField] float decceleration = 9f;
    //[SerializeField] float velPower = 1.2f;
    [SerializeField] float moveDelay = 0f;

    float currentSpeed;
    bool checkingTurnAround = true;
    bool reachedTurnAround;
    bool isAttacking;
    //private Animator anim;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkingTurnAround)
        {
            if (Physics2D.Raycast(rb.position, Vector2.right, 0.5f, turnAroundMask) || bodyCollider.IsTouchingLayers(turnAroundMask))
            {
                reachedTurnAround = true;
                checkingTurnAround = false;
                if(moveDelay > 0)StartCoroutine("DelayFlip");
                else
                {
                    reachedTurnAround = false;
                    Flip();
                    StartCoroutine("DelayPatrol");
                }
                
            }
            //anim.SetBool("Moving",true);
        }
    }

    IEnumerator DelayFlip()
    {
        yield return new WaitForSeconds(moveDelay);
        Flip();
        reachedTurnAround = false;
        StartCoroutine("DelayPatrol");
    }

    IEnumerator DelayPatrol()
    {
        yield return new WaitForSeconds(1f);
        checkingTurnAround = true;
    }

    private void FixedUpdate()
    {
        if (!isAttacking) Move();
    }

    void Move()
    {
        if (reachedTurnAround) currentSpeed = 0;
        else currentSpeed = speed;
        rb.MovePosition(rb.position + new Vector2(1, 0) * (Time.fixedDeltaTime * currentSpeed));

        /*float targetSpeed = currentSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate;
        accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);
        rb.AddForce(movement * Vector2.right);*/
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
    }

    public void Attacking(float attackCooldown, float chargeTime)
    {
        isAttacking = true;
        currentSpeed = 0;
        Invoke("DelayChargeAttack", chargeTime);
        Invoke("DelayAttack", attackCooldown);
    }

    void DelayChargeAttack()
    {
        anim.SetTrigger("attack");
    }

    void DelayAttack()
    {
        currentSpeed = speed;
        isAttacking = false;
    }

    public void StopMoving()
    {
        currentSpeed = 0;
    }
}


