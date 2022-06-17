using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [HideInInspector]public static ObjectPool instance { get; private set; }
    public int size;
    public GameObject[] prefabs;
    [SerializeField] private List<PoolObject> poolObjects;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InstantiateObjects();
    }

    void InstantiateObjects()
    {
        poolObjects = new List<PoolObject>();
        for (int i = 0; i < size; i++)
        {
            foreach (GameObject go in prefabs)
            {
                poolObjects.Add(Instantiate(go, transform).GetComponent<PoolObject>());
            }
        }
    }

    public PoolObject requestObject(PoolObjectType type)
    {
        foreach (PoolObject go in poolObjects)
        {
            if (go.type == type && !go.isActive())
            {
                return go;
            }
        }
        return null;
    }

    public void DeactivateAll()
    {
        foreach (PoolObject po in poolObjects)
        {
            po.deactivate();
        }
    }
}
