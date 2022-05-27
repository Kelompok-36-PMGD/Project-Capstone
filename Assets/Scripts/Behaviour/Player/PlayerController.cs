using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    float horizontal;
    float lastDirectionPressed;
    float runDelay;
    float lastWalkPressed;

    Animator anim;

    private void Awake()
    {
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
            if(lastDirectionPressed == 1f && lastWalkPressed > 0f)
            {
                playerMovement.running = true;
            }
            lastWalkPressed = runDelay;
            lastDirectionPressed = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (lastDirectionPressed == -1f && lastWalkPressed > 0f)
            {
                playerMovement.running = true;
            }
            lastWalkPressed = runDelay;
            lastDirectionPressed = -1f;
        }
    }

    public void Dead()
    {
        Debug.Log("Ded");
    }
}
