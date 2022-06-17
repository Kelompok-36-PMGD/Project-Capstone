using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButoIjoSound : MonoBehaviour
{
    Enemy_Melee enemy_Melee;

    private void Awake()
    {
        enemy_Melee = GetComponent<Enemy_Melee>();
    }

    public void ButoIjoAttackSound()
    {
        if(enemy_Melee)PlayerSound.instance.ButoIjoAttack();
    }
}
