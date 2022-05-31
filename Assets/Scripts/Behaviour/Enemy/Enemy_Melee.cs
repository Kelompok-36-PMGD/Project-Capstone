using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    Animator anim;
    Patrol patrol;
    [SerializeField] private bool enemyPatrol;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float distanceCollider;
    [SerializeField] private float attackHeight = 5f;
    [SerializeField] private float attackYOffset = 2f;
    [Header("Damage")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float chargeAttackDelay;
    [SerializeField] private int damage;

    private Life playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        if(enemyPatrol)patrol = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (CheckPlayer())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //attack
                DamageTrigger();
                cooldownTimer = 0;
                //anim
                //anim.SetTrigger("MeleeAttack");
            }
        }
    }

    private bool CheckPlayer()
    {
        //cek posisi pemain dengan raycast
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider + transform.up * attackYOffset, new Vector3(boxCollider.bounds.size.x * range, attackHeight, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Life>();
        }
            

        return hit.collider != null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider + transform.up * attackYOffset, new Vector3(boxCollider.bounds.size.x * range, attackHeight, boxCollider.bounds.size.z));
    }


    private void DamageTrigger()
    {
        if(enemyPatrol)patrol.Attacking(attackCooldown, chargeAttackDelay);
        else anim.SetTrigger("charge");
        //Delay damage for charge attack animation
        Invoke("DamagePlayer", chargeAttackDelay);
    }

    void DamagePlayer()
    {
        anim.SetTrigger("attack");
        //If the player still inside the enemy attack range, damage them
        if (CheckPlayer()) {
            playerHealth.OnHit(damage);
            PlayerController.instance.HitAnimation(); 
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            //anim.SetTrigger("Die");
        }

    }
   
}