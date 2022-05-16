using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "ScriptableInteger/Health")]
public class Health : ScriptableObject
{
    public int value;
    public int defaultValue;
    public int maxValue;
    public bool resetOnEnable;


    private void OnEnable()
    {
        if (resetOnEnable)
        {
            reset();
        }
    }

    public void reset()
    {
        value = defaultValue;
    }


}
