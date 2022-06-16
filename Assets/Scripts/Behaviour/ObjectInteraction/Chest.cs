using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Tooltip("Assign the corresponding chest key type (Silver or Golden)")]
    [SerializeField] GameObject keyTypePrefab;
    Animator anim;
    [Header("DropItem")]
    [Range(0, 100)] public int coinDrop;
    public List<GameObject> itemFixedDrops;
    public List<ObjectSpawnRate> itemChanceDrops;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    bool checkKey()
    {
        string keyName = keyTypePrefab.name;
        foreach(GameObject go in InventorySystem.instance.items)
        {
            if (go.name == keyName) return true;
        }
        return false;
    }
    int getKeyIndex()
    {
        string keyName = keyTypePrefab.name;
        int i = 0;
        foreach (GameObject go in InventorySystem.instance.items)
        {
            if (go.name == keyName) return i;
            i++;
        }
        return -1;
    }

    public void OpenChest()
    {
        int index = getKeyIndex();
        if (index >= 0)
        {
            Destroy(InventorySystem.instance.items[index], 0.1f);
            InventorySystem.instance.items.RemoveAt(index);
            Debug.Log("Player has the correct key and opened the chest, so the key disappeared");
            anim.SetTrigger("Open");
            ObjectPool.instance.requestObject(PoolObjectType.DropLauncher).gameObject.GetComponent<ObjectDropLauncher>().requestLauncher(transform, PoolObjectType.Coin, coinDrop);
            foreach(GameObject go in itemFixedDrops)
            {
                Instantiate(go, transform.position, Quaternion.identity);
            }

            //count all of the drops rate
            float rate = 0f;
            foreach (ObjectSpawnRate drop in itemChanceDrops)
            {
                rate += drop.rate;
            }
            float random = Random.Range(0, rate);
            foreach (ObjectSpawnRate drop in itemChanceDrops)
            {
                if (random <= drop.rate)
                {
                    Instantiate(drop.prefabs, transform.position, Quaternion.identity);
                }
                else
                {
                    random -= drop.rate;
                }
            }

            //Makes the chest uninteractable
            gameObject.layer = 0;
        }
        else Debug.Log("Key not detected");
    }

    void ChanceDrop()
    {

    }

}