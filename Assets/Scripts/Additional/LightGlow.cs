using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightGlow : MonoBehaviour
{
    public Light2D lights;
    public float multiplier = 2f;
    public bool valid = true;

    bool glow = false;
    private void Update()
    {
        if (glow)
        {
            if (lights.intensity < 1000000) lights.intensity = lights.intensity * multiplier;
        }
    }

    public void ToggleValid()
    {
        valid = valid ? false : true;
    }

    public void StartGlow()
    {
        if(valid) glow = true;
    }
}
