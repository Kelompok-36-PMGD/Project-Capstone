using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDamage : MonoBehaviour
{
    public bool randomXForce;
    public float deactiveTimer = 2f;
    [Header("Fonts")]
    public Font strongFont;
    public Font weakFont;
    public Font normalFont;
    Rigidbody2D rb;
    TextMesh text;
    MeshRenderer meshRenderer;
    bool decrease;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        text = GetComponent<TextMesh>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        rb.velocity = new Vector2(0, 0);
        float x = -1;
        if (randomXForce) x = Random.Range(-4, 4);
        rb.AddForce(new Vector2(x, 12f), ForceMode2D.Impulse);
        Invoke("DelayDeactivate", deactiveTimer);
    }

    private void Update()
    {
        //gradually decrease the alpha
        if(decrease) text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a,0,3f * Time.deltaTime));
    }

    void DecreaseAlpha()
    {
        decrease = true;
    }

    public void Damage(int damage, Weakness weakness)
    {
        text.text = damage.ToString();
        decrease = false;
        Invoke("DecreaseAlpha", 1f);
        switch (weakness)
        {
            case Weakness.NORMAL:
                text.color = Color.white;
                text.font = normalFont;
                meshRenderer.material = normalFont.material;
                break;
            case Weakness.STRONG:
                text.color = Color.red;
                text.font = strongFont;
                meshRenderer.material = strongFont.material;
                break;
            case Weakness.WEAK:
                text.color = Color.yellow;
                text.font = weakFont;
                meshRenderer.material = weakFont.material;
                break;
            default:
                Debug.Log("No weakness found");
                break;
        }
    }

    void DelayDeactivate()
    {
        gameObject.SetActive(false);
    }
}
