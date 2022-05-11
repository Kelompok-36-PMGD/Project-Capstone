using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public int groundLayer;
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float acceleration = 4f;
    [SerializeField] float decceleration = 9f;
    [SerializeField] float velPower = 1.2f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 20f;
    [Tooltip("Compensate the player while pressing jump in midAir")]
    [SerializeField] float jumpBuffer = 0.5f;
    [Tooltip("How much time the player will be jumping when jump button is released")]
    [SerializeField] float jumpEndEarlyMinimal = 0.2f;
    [SerializeField] float jumpEndEarlyModifier = 0.3f;
    [Tooltip("Compensate the player while pressing jump in midAir a few seconds after ungrounded")]
    [SerializeField] float coyoteJumpTime = 0.2f;
    [Tooltip("Set value from 0 to 1.")]
    [SerializeField] float airHorizontalDrag = 0.2f;

    float coyoteTime;
    float jumpEndEarlyTime;
    float jumpBufferTime;

    bool jumping;
    bool jumpButtonReleased;
    bool endedJumpEarly;

    float horizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Get Input as float
        horizontal = Input.GetAxisRaw("Horizontal");

        //Time dependant variables
        if (isGrounded()) coyoteTime = coyoteJumpTime; //Coyote time will remain true so the player could jump anytime the player touches the ground.
        else coyoteTime -= Time.deltaTime; //Coyote time will count a few seconds after ungrounded
        jumpBufferTime -= Time.deltaTime;
        jumpEndEarlyTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpBufferTime = jumpBuffer;
            jumpButtonReleased = false;
            if (coyoteTime > 0f && isGrounded())
            {
                jumping = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            jumpButtonReleased = true;
        }

        //Jump Ended Early
        if (jumpButtonReleased && jumpEndEarlyTime <= 0f && rb.velocity.y > 0) endedJumpEarly = true;
        else endedJumpEarly = false;
    }

    bool hasBufferedJump => isGrounded() && jumpBufferTime > 0f;

    private void FixedUpdate()
    {
        //Player movement physics
        float targetSpeed = horizontal * speed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate;
        //Apply air drag while midAir, false otherwise
        if (!isGrounded()) accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration * airHorizontalDrag; 
        else accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);
        rb.AddForce(movement * Vector2.right);

        //Jump if onKeyDown or has buffered jump(pressing jump while midAir)
        if(jumping || hasBufferedJump)
        {
            jumpBufferTime = 0f;
            jumping = false;
            Jump();
        }
        //Drag the player down if the jump button is released
        if (endedJumpEarly) rb.velocity = !isGrounded() && rb.velocity.y > 0 ? new Vector2(rb.velocity.x, rb.velocity.y * jumpEndEarlyModifier) : rb.velocity;
    }

    void Jump()
    {
        jumpEndEarlyTime = jumpEndEarlyMinimal; //Reset the timer so that the player will jump at least until this value < 0
        rb.velocity = new Vector2(rb.velocity.x, 0f); //Reset the jump so that the added Force stay constant
        rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
    }

    bool isGrounded()
    {
        int groundMask = 1 << groundLayer;
        return Physics2D.Raycast(rb.position, Vector2.down, transform.localScale.y + 0.01f, groundMask);
    }
}
