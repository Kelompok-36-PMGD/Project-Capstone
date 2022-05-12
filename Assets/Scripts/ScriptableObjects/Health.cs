using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "ScriptableInteger/Health")]
public class Health : ScriptableObject
{
    [SerializeField] int health;

    public void takeDamage(int amount)
    {
        health -= amount;
    }

    public void gainHealth(int amount)
    {
        health += amount;
    }
    
}
