using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    PlayerMovement playerMovement;
    float horizontal;
    float lastDirectionPressed;
    float runDelay;
    float lastWalkPressed;

    Animator anim;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        runDelay = playerMovement.runKeyPressDelay;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        playerMovement.horizontal = horizontal;

        lastWalkPressed -= Time.deltaTime;

        //WalkRight
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("walk", true);
            if(lastDirectionPressed == 1f && lastWalkPressed > 0f)
            {
                //Run animation on PlayerMovement scripts for convenient
                playerMovement.running = true;
            }
            lastWalkPressed = runDelay;
            lastDirectionPressed = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("walk", true);
            if (lastDirectionPressed == -1f && lastWalkPressed > 0f)
            {
                playerMovement.running = true;
            }
            lastWalkPressed = runDelay;
            lastDirectionPressed = -1f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) && lastWalkPressed < 0f)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
    }

    public void Dead()
    {
        Debug.Log("Ded");
    }

    public void HitAnimation()
    {
        Debug.Log("Kena hit");
        //anim.SetTrigger("hit");
    }
}
