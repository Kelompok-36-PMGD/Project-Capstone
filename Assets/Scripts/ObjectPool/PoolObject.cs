using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType { DropLauncher, Coin, Potion, SilverKey, GoldenKey, SkillObject}
public class PoolObject : MonoBehaviour
{
    public PoolObjectType type;
    public bool deactiveOnStart = true;
    void Awake()
    {
        if(deactiveOnStart)deactivate();
    }

    public void activate(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
    public bool isActive()
    {
        return gameObject.activeInHierarchy;
    }
}
