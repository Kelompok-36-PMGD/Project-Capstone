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

    [HideInInspector] public bool isDeath;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        runDelay = playerMovement.runKeyPressDelay;
    }

    private void Start()
    {
        GameManager.instance.LoadDataFromGameManager();
    }

    private void Update()
    {
        if(!isDeath)horizontal = Input.GetAxisRaw("Horizontal");
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
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
    }

    public void Dead()
    {
        DeathAnimation();
        PlayerMovement.instance.ResetVelocity();
        PlayerMovement.instance.enabled = false;
        gameObject.GetComponent<Skills>().enabled = false;
        InventorySystem.instance.enabled = false;
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
        Debug.Log("Player is death, currently the movement, skills, and inventory is disabled, and change the tag and layer to default to prevent unintended behaviour. This should shows up a restart confirmation");
    }

    public void HitAnimation()
    {
        anim.SetTrigger("hit");
    }

    public void DeathAnimation()
    {
        anim.SetBool("death", true);
    }

    public bool SkillReady()
    {
        if (playerMovement.checkGrounded())
        {
            return true;
        }
        return false;
    }

    public void UseSkill()
    {
        anim.SetTrigger("skill");
        playerMovement.UseSkill();
    }

    public void DropCoin()
    {
        ObjectPool.instance.requestObject(PoolObjectType.DropLauncher).GetComponent<ObjectDropLauncher>().requestOneLauncher(transform, PoolObjectType.Coin).GetComponent<Coin>().SetDelayPick();
    }
}
