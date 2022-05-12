using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    float horizontal;

    Animator anim;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        playerMovement.horizontal = horizontal;
    }

    public void Dead()
    {
        Debug.Log("Ded");
    }
}
