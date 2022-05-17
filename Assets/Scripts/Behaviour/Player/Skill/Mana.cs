using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    [Header("Objects")]
    public Health mana;
    public Slider slider;
    public Slider movingSlider;
    public float _sliderSpeed = 5f;
    public ParticleSystem _particleSys;
    [Header("Regenerate")]
    public bool _RegenOverTime = true;
    public int _regenValue = 1;
    [Range(0,20)]public float _regenRate = 2f;

    float rate=0f;
    ParticleSystem.EmissionModule emission;
    private void Awake()
    {
        emission = _particleSys.emission;
        slider.maxValue = mana.maxValue;
        movingSlider.maxValue = mana.maxValue;
        slider.value = mana.value;
        movingSlider.value = slider.value;
    }

    private void Update()
    {
        slider.value = mana.value;
        //Move the slider overtime until it reach the same as the mana value
        if (slider.value < movingSlider.value)
        {

            emission.rateOverTime = 10;
            movingSlider.value -= Time.deltaTime * _sliderSpeed;
            //movingSlider.value = movingSlider.value - _sliderSpeed < slider.value ? movingSlider.value = mana.value : movingSlider.value - mana.value;
        }
        else
        {
            movingSlider.value = mana.value;
            emission.rateOverTime = 0;
        }


        if (_RegenOverTime)
        {
            //Mana calculation is : Mana per seconds = RegenRate * regenValue. (ex: regenRate 5 and regenValue 2 is equal to 10 mana per seconds
            rate += Time.deltaTime * _regenRate;
            if(rate > 1f)
            {
                mana.value = mana.value + _regenValue > mana.maxValue ? mana.maxValue : mana.value + _regenValue;
                rate = 0;
            }
        }
    }

    public void GainMana(int value)
    {
        mana.value = mana.value + value > mana.maxValue ? mana.maxValue : mana.value + value;
    }

    public void Decrease(int value)
    {
        if(mana.value - value > 0)
        {
            mana.value -= value;
        }
        else
        {
            //show not enough mana;
            Debug.Log("Not enough mana");
        }
    }
}
