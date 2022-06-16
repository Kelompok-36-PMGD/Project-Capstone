using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : MonoBehaviour
{
    Animator anim;
    Patrol patrol;
    [SerializeField] private bool enemyPatrol;
    [SerializeField] private float range;
    public GameObject bulletPrefabs;
    public GameObject bulletSpawnLocation;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float distanceCollider;
    [SerializeField] private float attackHeight = 5f;
    [SerializeField] private float attackYOffset = 2f;
    [Header("Damage")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float chargeAttackDelay;

    float xScale;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (enemyPatrol) patrol = GetComponent<Patrol>();
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (CheckPlayer())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //attack
                cooldownTimer = 0;
                DamageTrigger();
                //anim
                //anim.SetTrigger("MeleeAttack");
            }
        }
    }

    private bool CheckPlayer()
    {
        //cek posisi pemain dengan raycast
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider + transform.up * attackYOffset, new Vector3(boxCollider.bounds.size.x * range, attackHeight, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        //if (hit.collider != null)


        return hit.collider != null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distanceCollider + transform.up * attackYOffset, new Vector3(boxCollider.bounds.size.x * range, attackHeight, boxCollider.bounds.size.z));
    }
    private void DamageTrigger()
    {
        anim.SetTrigger("charge");
        if (enemyPatrol) patrol.Attacking(attackCooldown, chargeAttackDelay);
        //Delay damage for charge attack animation
        Invoke("DamagePlayer", chargeAttackDelay);
    }

    private void DamagePlayer()
    {
        anim.SetTrigger("attack");
        //spawn bullet
        xScale = transform.localScale.x;
        Instantiate(bulletPrefabs, bulletSpawnLocation.transform.position, Quaternion.identity).gameObject.GetComponent<MoveForward>().SetXScale(xScale);
    }
    public void OnHitEnemyStun()
    {
        cooldownTimer = 0;
        CancelInvoke("DamagePlayer");
    }
}
