using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Life))]
public class EnemyController : MonoBehaviour
{
    [Header("Weaknesses")]
    public List<EnemyWeakness> enemyWeaknesses;

    [Header("Enemy Settings")]
    private Life life;
    public Transform damageText;

    Animator anim;

    private void Awake()
    {
        life = GetComponent<Life>();
        anim = GetComponent<Animator>();
    }

    //Use this method to decrease enemy health when attacked by player instead of directly call Life.OnHit
    public void TakeDamage(float value,AttackType type)
    {
        float multiplier = 0f;
        Weakness weakness = Weakness.NORMAL;
        //bad performance wise code, maybe need to change
        foreach(EnemyWeakness eW in enemyWeaknesses)
        {
            if (type == eW.attackType)
            {
                multiplier = eW.getModifier(type);
                weakness = eW.weakness;
            }
        }
        int damage = (int)Mathf.Ceil(value * multiplier);
        life.OnHit(damage);
        anim.SetTrigger("hit");
        ShowDamageText(damage, weakness);
    }

    public void DeadAnimation()
    {
        anim.SetBool("death", true);
    }

    public void EnemyDeath(float time)
    {
        Invoke("DestroyGameObject", time);
    }

    void DestroyGameObject()
    {
        Destroy(transform.parent.gameObject);
    }

    public void ShowDamageText(int damage, Weakness weakness)
    {
        GameObject go = ObjectPool.instance.requestObject(PoolObjectType.DamageText).gameObject;
        go.transform.position = damageText.position;
        go.GetComponent<TextDamage>().Damage(damage, weakness);
        go.SetActive(true);
    }

    public void PortalCountDefeat()
    {
        Portal.instance.MinionDefeated();
    }
}
