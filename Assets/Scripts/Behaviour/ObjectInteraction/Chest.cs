using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Tooltip("Assign the corresponding chest key type (Silver or Golden)")]
    [SerializeField] GameObject keyTypePrefab;
    [Range(0,100)]public int coinDrop;
    Animator anim;

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
            InventorySystem.instance.items.RemoveAt(index);
            Debug.Log("Player has the correct key and opened the chest, so the key disappeared");
            anim.SetTrigger("Open");
            ObjectPool.instance.requestObject(PoolObjectType.DropLauncher).gameObject.GetComponent<ObjectDropLauncher>().requestLauncher(transform, PoolObjectType.Coin, coinDrop);
        }
        else Debug.Log("Key not detected");
    }


}
