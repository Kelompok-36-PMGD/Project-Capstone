using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleFrame : MonoBehaviour
{
    public float duration;
    public float timer;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime < 0 ? 0 : timer - Time.deltaTime;
    }

    public void activate()
    {
        if (!isActive)
        {
            timer = duration;
            StartCoroutine(startBlinking());
            deactivateCollider();
        }
    }

    private void deactivateCollider()
    {
        Physics2D.IgnoreLayerCollision(7, 0, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 11, true);
        Physics2D.IgnoreLayerCollision(7, 12, true);
    }
    public void activateCollider()
    {
        Physics2D.IgnoreLayerCollision(7, 0, false);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 11, false);
        Physics2D.IgnoreLayerCollision(7, 12, false);
    }

    private IEnumerator startBlinking()
    {
        isActive = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color defaultColor = sr.color;
        Color hit = defaultColor;
        hit.a = 0.3f;

        while (timer > 0)
        {
            sr.color = hit;
            yield return new WaitForSeconds(0.2f);
            sr.color = default;
            yield return new WaitForSeconds(0.2f);
        }
        sr.color = defaultColor;
        activateCollider();
        isActive = false;
    }
}
