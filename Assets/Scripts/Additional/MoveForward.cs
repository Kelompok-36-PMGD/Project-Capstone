using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float moveSpeed;
    float xScale;
    float direction;

    private void Update()
    {
        transform.Translate(transform.right * moveSpeed * direction * Time.deltaTime);
    }

    public void SetXScale(float value)
    {
        transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        xScale = transform.localScale.x;
        if (xScale > 0) direction = 1f;
        else direction = -1f;
    }

}
