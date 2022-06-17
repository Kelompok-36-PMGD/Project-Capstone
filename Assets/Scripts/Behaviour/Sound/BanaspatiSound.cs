using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanaspatiSound : MonoBehaviour
{
    private void Awake()
    {
        PlayerSound.instance.BanaspatiBullet();
    }
}
