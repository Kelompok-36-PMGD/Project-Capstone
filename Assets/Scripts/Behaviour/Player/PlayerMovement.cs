using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
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

    [Header("Dash")]
    [SerializeField] float dashCooldown = 2f;
    [Tooltip("The player could stay in Anti gravity for set amount of time")]
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashForce = 10f;

    [Header("Run")]
    public float runSpeed = 10f;
    public float runKeyPressDelay = 1f;
    public float runChangeDirectionCompensation = 0.5f;

    float coyoteTime;
    float jumpEndEarlyTime;
    float jumpBufferTime;
    float dashCooldownTimer;
    float dashTimer;

    bool falling;
    bool jumping;
    bool jumpButtonReleased;
    bool endedJumpEarly;
    bool doubleJump;
    bool isGrounded;
    bool doubleJumpIsValid;
    float runChangeDirectionTimer;
    bool isUsingSkill;

    [HideInInspector]public float horizontal;
    [HideInInspector]public bool running;
    [HideInInspector]public bool isClimbing = false;
    float direction; //Track the current direction the player headed
    [HideInInspector] public int facingDirection;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingDirection = 1;
    }

    private void Update()
    {
        isGrounded = checkGrounded();

        //Animation
        //Check landing
        if (isGrounded) anim.SetBool("landing", true);

        //Flip
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
        }

        //Get Input as float
        //Tracking last face direction before dash
        if (horizontal > 0 && dashTimer < 0f) //Player is facing right
        {
            direction = 1f;
        }
        else if(horizontal < 0 && dashTimer <0f) //Player is facing left
        {
            direction = -1f;
        }

        //run Stop
        if (running && horizontal == 0f && runChangeDirectionTimer < 0f)
        {
            running = false;
            runChangeDirectionTimer = runChangeDirectionCompensation;
        }
        else if (!running & horizontal != 0f && runChangeDirectionTimer > 0f)
        {
            running = true;
            runChangeDirectionTimer = 0f; //reset
        }
        //Time dependant variables
        if (isGrounded)
        {
            doubleJumpIsValid = true;
            coyoteTime = coyoteJumpTime; //Coyote time will remain true so the player could jump anytime the player touches the ground.
        } 
        else coyoteTime -= Time.deltaTime; //Coyote time will count a few seconds after ungrounded
        jumpBufferTime -= Time.deltaTime;
        jumpEndEarlyTime -= Time.deltaTime;
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        runChangeDirectionTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTime = jumpBuffer;
            jumpButtonReleased = false;
            if (coyoteTime > 0f && isGrounded)
            {
                jumping = true;
            }
            if(doubleJumpIsValid && !isGrounded && dashTimer <0f)
            {
                doubleJump = true;
                doubleJumpIsValid = false;
            }
            //Buffer jump while dashing
            else if(dashTimer > 0f && doubleJumpIsValid)
            {
                hasBufferedDoubleJump = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpButtonReleased = true;
        }
        /*if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            dashCooldownTimer = dashCooldown;
            dashTimer = dashDuration;
        }*/

        //Jump Ended Early
        if (jumpButtonReleased && jumpEndEarlyTime <= 0f && rb.velocity.y > 0) endedJumpEarly = true;
        else endedJumpEarly = false;
    }

    bool hasBufferedJump => isGrounded && jumpBufferTime > 0f;
    bool hasBufferedDoubleJump;

    private void FixedUpdate()
    {
        //Player movement physics
        float targetSpeed = horizontal * speed;
        if (running)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", true);
            targetSpeed = horizontal * runSpeed;
        }
        else if (horizontal == 0)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
        //Stop moving when using skill
        if (isUsingSkill)
        {
            targetSpeed = 0;
        }
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate;
        //Apply air drag while midAir, false otherwise
        if (!isGrounded) accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration * airHorizontalDrag; 
        else accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);
        rb.AddForce(movement * Vector2.right);

        //Animation Falling
        if (rb.velocity.y < 0 && !isClimbing) falling = true;
        else falling = false;
        anim.SetBool("fall", falling);


        //Jump if onKeyDown or has buffered jump(pressing jump while midAir)
        if (jumping || hasBufferedJump || doubleJump)
        {
            if (!isUsingSkill)
            {
                jumpBufferTime = 0f;
                jumping = false;
                doubleJump = false;
                Jump();
            }
        }
        else if (hasBufferedDoubleJump && dashTimer < 0f)
        {
            doubleJump = false;
            hasBufferedDoubleJump = false;
            Jump();
        }
        //Drag the player down if the jump button is released
        if (endedJumpEarly) rb.velocity = !isGrounded && rb.velocity.y > 0 ? new Vector2(rb.velocity.x, rb.velocity.y * jumpEndEarlyModifier) : rb.velocity;

        //Dash
        if (dashTimer > 0f)
        {
            //Make the player in anti gravity state for a short period of time while dashing
            rb.velocity = new Vector2(direction * dashForce, 0f); //Use this method if u guys prefer constant speed while dashing rather than using Force
            //rb.AddForce(new Vector2(direction * dashForce, 0), ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        anim.SetTrigger("jump");
        jumpEndEarlyTime = jumpEndEarlyMinimal; //Reset the timer so that the player will jump at least until this value < 0
        rb.velocity = new Vector2(rb.velocity.x, 0f); //Reset the jump so that the added Force stay constant
        rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
    }

    public bool checkGrounded()
    {
        int groundMask = 1 << groundLayer;
        return Physics2D.Raycast(rb.position, Vector2.down, transform.localScale.y + 0.01f, groundMask);
    }

    public void UseSkill()
    {
        isUsingSkill = true;
        Invoke("SkillDelay", 0.6f);
    }

    void SkillDelay()
    {
        isUsingSkill = false;
    }

    public void ResetVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }
}