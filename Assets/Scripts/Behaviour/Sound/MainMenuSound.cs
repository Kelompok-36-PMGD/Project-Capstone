using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    public void ClickSound()
    {
        PlayerSound.instance.ClickButtonSound();
    }
}
