using System;
using UnityEngine;

[Serializable]
public class SkillList
{
    public AttackType attackType;
    [Range(0,100)] public int manaCost;
    public int damage;
}
