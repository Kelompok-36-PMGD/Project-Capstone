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
        xScale = transform.localScale.x;
        direction = Mathf.Sign(value);
        transform.localScale = new Vector3(xScale * direction, transform.localScale.y, transform.localScale.z);
    }

}
