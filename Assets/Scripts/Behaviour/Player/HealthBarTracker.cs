using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTracker : MonoBehaviour
{
    [Header("Objects")]
    public Health playerHealth;
    public Slider slider;
    public Slider movingSlider;
    public float _sliderSpeed = 5f;
    public ParticleSystem _particleSys;

    ParticleSystem.EmissionModule emission;

    private void Awake()
    {
        emission = _particleSys.emission;
        slider.maxValue = playerHealth.maxValue;
        movingSlider.maxValue = playerHealth.maxValue;
        slider.value = playerHealth.value;
        movingSlider.value = slider.value;
    }

    private void Update()
    {
        slider.value = playerHealth.value;
        //Move the slider overtime until it reach the same as the mana value
        if (slider.value < movingSlider.value)
        {

            emission.rateOverTime = 10;
            movingSlider.value -= Time.deltaTime * _sliderSpeed;
            //movingSlider.value = movingSlider.value - _sliderSpeed < slider.value ? movingSlider.value = mana.value : movingSlider.value - mana.value;
        }
        else
        {
            movingSlider.value = playerHealth.value;
            emission.rateOverTime = 0;
        }


        /*if (_RegenOverTime)
        {
            //Mana calculation is : Mana per seconds = RegenRate * regenValue. (ex: regenRate 5 and regenValue 2 is equal to 10 mana per seconds
            rate += Time.deltaTime * _regenRate;
            if (rate > 1f)
            {
                playerHealth.value = playerHealth.value + _regenValue > playerHealth.maxValue ? playerHealth.maxValue : playerHealth.value + _regenValue;
                rate = 0;
            }
        }*/
    }
}
