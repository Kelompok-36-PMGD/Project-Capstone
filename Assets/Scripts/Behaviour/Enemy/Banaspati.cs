using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banaspati : MonoBehaviour
{
    Animator anim;
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
    [SerializeField] private int bulletPerAttack = 5;
    [SerializeField] private float delayPerBullet = 1f;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float chargeAttackDelay;

    float xScale;
    int count = 0;
    bool isAttacking;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (CheckPlayer())
        {
            if (cooldownTimer >= attackCooldown && !isAttacking)
            {
                isAttacking = true;
                DamageTrigger();
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
        anim.SetBool("charge", true);
        //Delay damage for charge attack animation
        Invoke("DamagePlayer", chargeAttackDelay);
    }

    private void DamagePlayer()
    {
        count = 0;
        //spawn bullet
        xScale = transform.localScale.x;
        InvokeRepeating("SpawnBullet", 0.1f, delayPerBullet);
    }

    private void SpawnBullet()
    {
        if (count < bulletPerAttack)
        {
            Instantiate(bulletPrefabs, bulletSpawnLocation.transform.position, bulletSpawnLocation.transform.rotation).gameObject.GetComponent<MoveForward>().SetXScale(xScale);
            count++;
        }
        else
        {
            CancelInvoke("SpawnBullet");
            anim.SetBool("charge", false);
            cooldownTimer = 0;
            isAttacking = false;
        }
        
    }

    public void ActivateBanaspati()
    {
        gameObject.SetActive(true);
    }

    public void DestroyBanaspati()
    {
        Destroy(gameObject);
    }
}
