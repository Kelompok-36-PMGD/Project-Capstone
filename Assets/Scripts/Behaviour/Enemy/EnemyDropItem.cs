using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public List<ObjectSpawnRate> drops;
    [Tooltip("How many is the drops?")]
    [Range(1,100)] public float rolls;

    public void dropOnDeath()
    {
        //count all of the drops rate
        float rate = 0f;
        foreach (ObjectSpawnRate drop in drops)
        {
            rate += drop.rate;
        }

        //Repeatedly gacha
        for (int i = 0; i < rolls; i++)
        {
            float random = Random.Range(0, rate);
            foreach(ObjectSpawnRate drop in drops)
            {
                if(random <= drop.rate)
                {
                    spawnObject(drop.prefabs);
                }
                else
                {
                    random -= drop.rate;
                }
            }
        }
    }

    void spawnObject(GameObject obj)
    {
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
