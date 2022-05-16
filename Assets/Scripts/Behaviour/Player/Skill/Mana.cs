using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public Health mana;
    [Header("Regenerate")]
    public bool RegenOverTime = true;
    public int regenValue = 1;
    [Range(0,20)]public float regenRate = 2f;

    float rate=0f;

    private void Update()
    {
        if (RegenOverTime)
        {
            //Mana calculation is : Mana per seconds = RegenRate * regenValue. (ex: regenRate 5 and regenValue 2 is equal to 10 mana per seconds
            rate += Time.deltaTime * regenRate;
            if(rate > 1f)
            {
                mana.value = mana.value + regenValue > mana.maxValue ? mana.maxValue : mana.value + regenValue;
                rate = 0;
            }
        }
    }

    void GainMana(int value)
    {
        mana.value = mana.value + value > mana.maxValue ? mana.maxValue : mana.value + value;
    }

    void Decrease(int value)
    {
        mana.value = mana.value - value < 0 ? 0 : mana.value - value;
    }
}
