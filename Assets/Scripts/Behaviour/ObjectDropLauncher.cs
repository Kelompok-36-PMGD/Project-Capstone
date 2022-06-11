using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDropLauncher : MonoBehaviour
{
    PoolObjectType _type;
    int count;
    int _amount;
    public void requestLauncher(Transform pos, PoolObjectType type, int amount)
    {
        count = 0;
        transform.position = pos.position;
        _type = type;
        _amount = amount;
        InvokeRepeating("delayLaunch", 0.5f,0.05f);
    }

    void delayLaunch()
    {
        if (count < _amount)
        {
            count++;
            GameObject go = ObjectPool.instance.requestObject(_type).gameObject;
            go.transform.position = transform.position;
            go.SetActive(true);
        }
        else CancelInvoke("delayLaunch");
    }

    public GameObject requestOneLauncher(Transform pos, PoolObjectType type)
    {
        count = 0;
        transform.position = pos.position;
        _type = type;
        _amount = 1;
        GameObject go = ObjectPool.instance.requestObject(_type).gameObject;
        go.transform.position = transform.position;
        go.SetActive(true);
        return go;
    }
}
