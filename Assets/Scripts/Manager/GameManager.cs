using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Health coinScriptable;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
