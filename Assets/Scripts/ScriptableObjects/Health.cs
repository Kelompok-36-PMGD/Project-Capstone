using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "ScriptableInteger/Health")]
public class Health : ScriptableObject
{
    public int value;
    public int initialValue;
    public int defaultValue;
    public int maxValue;
    public int maxDefaultValue;
    public bool resetDefaultOnEnable;
    public bool resetInitialOnEnable;


    private void OnEnable()
    {
        if (resetDefaultOnEnable)
        {
            resetDefault();
        }
        else if (resetInitialOnEnable) resetInitial();
    }

    public void resetDefault()
    {
        value = defaultValue;
        maxValue = maxDefaultValue;
    }

    //Perfect for save/load system or checkpoint
    public void resetInitial()
    {
        value = initialValue;
    }

    public void setInitialValue(int value)
    {
        initialValue = value;
    }

}
