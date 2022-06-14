using UnityEngine;
using System;

public enum Weakness
{
    STRONG, NORMAL, WEAK
}

[Serializable]
public class EnemyWeakness
{
    //showInInspector
    [SerializeField]public AttackType attackType;
    [SerializeField]public Weakness weakness;

    public float getModifier(AttackType type)
    {
        if (weakness == Weakness.STRONG) return 2f;
        else if (weakness == Weakness.NORMAL) return 1f;
        else if (weakness == Weakness.WEAK) return 0.5f;
        return 0f;
    }
}
