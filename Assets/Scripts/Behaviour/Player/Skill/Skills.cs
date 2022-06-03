using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    //Change skills name here
    NORMAL, SKILL1, SKILL2, SKILL3
}

[RequireComponent(typeof(Mana))]
public class Skills : MonoBehaviour
{
    private Mana mana;
    [Header("NormalAttack")]
    public SkillList _normalAttack;
    public float _normalFireRate = 0.5f;
    [Header("Skills")]
    public SkillList _currentSkill;
    public List<SkillList> _skillList;

    float fireRateTimer = 0f;

    private void Awake()
    {
        mana = GetComponent<Mana>();
        _currentSkill = _skillList[0];
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Usekhodam & damage enemy if on range
            if (mana.Decrease(_currentSkill.manaCost)) {
                if(PlayerMovement.instance.facingDirection == 1) ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setSkill(_currentSkill, new Vector2(transform.position.x + 1f, transform.position.y), PlayerMovement.instance.facingDirection); 
                else ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setSkill(_currentSkill, new Vector2(transform.position.x - 1f, transform.position.y), PlayerMovement.instance.facingDirection);
            }
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            //Normal attack
            if(fireRateTimer >= _normalFireRate)
            {
                fireRateTimer = 0f;
                if(PlayerMovement.instance.facingDirection == 1) ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setNormal(_normalAttack, new Vector2(transform.position.x + 1f, transform.position.y),PlayerMovement.instance.facingDirection);
                else ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setNormal(_normalAttack, new Vector2(transform.position.x - 1f, transform.position.y), PlayerMovement.instance.facingDirection);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) _currentSkill = _skillList[0];
        else if (Input.GetKeyDown(KeyCode.Alpha2)) _currentSkill = _skillList[1];
        else if (Input.GetKeyDown(KeyCode.Alpha3)) _currentSkill = _skillList[2];
        else if (Input.GetKeyDown(KeyCode.Alpha4)) _currentSkill = _skillList[3];
    }
}
