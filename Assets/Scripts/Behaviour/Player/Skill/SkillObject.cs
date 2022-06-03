using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    SkillList _currentSkill;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    public GameObject normalAttack;
    public List<GameObject> SkillPrefabs;
    [Header("Attribute")]
    public float skillSpeed = 10f;
    public float deactivateCountdown = 10f;
    int direction=1;

    bool isSkill;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.Translate(transform.right * skillSpeed * direction * Time.deltaTime);
    }

    public void setNormal(SkillList normal,Vector2 pos, int dir)
    {
        _currentSkill = normal;
        transform.position = pos;
        anim.runtimeAnimatorController = normalAttack.GetComponent<Animator>().runtimeAnimatorController;
        spriteRenderer.sprite = normalAttack.GetComponent<SpriteRenderer>().sprite;
        boxCollider2D.offset = normalAttack.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = normalAttack.GetComponent<BoxCollider2D>().size;
        gameObject.SetActive(true);
        Invoke("Deactivate", deactivateCountdown);
        isSkill = false;
        direction = dir;
        if (direction == 1) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }

    public void setSkill(SkillList skill, Vector2 pos, int dir)
    {
        transform.position = pos;
        _currentSkill = skill;
        foreach(GameObject go in SkillPrefabs)
        {
            if (_currentSkill.attackType.ToString() == go.name)
            {
                anim.runtimeAnimatorController = go.GetComponent<Animator>().runtimeAnimatorController;
                spriteRenderer.sprite = go.GetComponent<SpriteRenderer>().sprite;
                boxCollider2D.offset = go.GetComponent<BoxCollider2D>().offset;
                boxCollider2D.size = go.GetComponent<BoxCollider2D>().size;
                gameObject.SetActive(true);
                Invoke("Deactivate", deactivateCountdown);
                isSkill = true;
                direction = dir;
                if (direction == 1) spriteRenderer.flipX = false;
                else spriteRenderer.flipX = true;
                return;
            }
        }
        Debug.Log("NoSkill found in SkillObject.setSkill() script");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") { }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (isSkill) collision.gameObject.GetComponent<EnemyController>().TakeDamage(_currentSkill.damage, _currentSkill.attackType);
            else collision.gameObject.GetComponent<EnemyController>().TakeDamage(_currentSkill.damage, _currentSkill.attackType);
            CancelInvoke("Deactivate");
            gameObject.SetActive(false);
        }
        else
        {
            CancelInvoke("Deactivate");
            gameObject.SetActive(false);
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
