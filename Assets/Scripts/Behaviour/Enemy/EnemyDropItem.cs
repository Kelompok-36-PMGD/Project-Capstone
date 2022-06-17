using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public List<ObjectSpawnRate> drops;
    [Tooltip("How many is the drops?")]
    [Range(1,100)] public float rolls;
    [Range(0, 100)] public int coinDrop;

    public void dropOnDeath()
    {
        //Drop Coin first
        ObjectPool.instance.requestObject(PoolObjectType.DropLauncher).gameObject.GetComponent<ObjectDropLauncher>().requestLauncher(transform, PoolObjectType.Coin, coinDrop);
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
