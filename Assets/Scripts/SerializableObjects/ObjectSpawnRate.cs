using UnityEngine;
using System;

[Serializable]
public class ObjectSpawnRate
{
    public GameObject prefabs;
    [Range(1, 100)] public int rate;
}
