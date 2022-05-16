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

    private void Awake()
    {
        life = GetComponent<Life>();
    }

    //Use this method to decrease enemy health when attacked by player instead of directly call Life.OnHit
    public void TakeDamage(float value,AttackType type)
    {
        float multiplier = 0f;
        //bad performance wise code, maybe need to change
        foreach(EnemyWeakness eW in enemyWeaknesses)
        {
            if (type == eW.attackType) multiplier = eW.getModifier(type);
        }
        life.OnHit(((int)Mathf.Ceil(value * multiplier)));
    }

    public void Dead()
    {
        Debug.Log("Ded");
    }
}