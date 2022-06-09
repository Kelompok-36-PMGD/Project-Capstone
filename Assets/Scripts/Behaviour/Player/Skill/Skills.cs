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
    public float _animDelay = 0.3f;
    public float _skillCooldown = 0.5f;

    float fireRateTimer = 0f;
    float skillTimer = 0f;

    private void Awake()
    {
        mana = GetComponent<Mana>();
        _currentSkill = _skillList[0];
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        skillTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Usekhodam & damage enemy if on range
            if (mana.DecreaseCheck(_currentSkill.manaCost)) {
                if (PlayerController.instance.SkillReady() && skillTimer >= _skillCooldown)
                {
                    PlayerController.instance.UseSkill();
                    skillTimer = 0f;
                    fireRateTimer = 0f;
                    Invoke("SkillDelay", _animDelay);
                }
            }
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            //Normal attack
            if(fireRateTimer >= _normalFireRate)
            {
                fireRateTimer = 0f;
                skillTimer = 0f;
                if(PlayerMovement.instance.facingDirection == 1) ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setNormal(_normalAttack, new Vector2(transform.position.x + 1f, transform.position.y),PlayerMovement.instance.facingDirection);
                else ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setNormal(_normalAttack, new Vector2(transform.position.x - 1f, transform.position.y), PlayerMovement.instance.facingDirection);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) _currentSkill = _skillList[0];
        else if (Input.GetKeyDown(KeyCode.Alpha2)) _currentSkill = _skillList[1];
        else if (Input.GetKeyDown(KeyCode.Alpha3)) _currentSkill = _skillList[2];
        else if (Input.GetKeyDown(KeyCode.Alpha4)) _currentSkill = _skillList[3];
    }

    void SkillDelay()
    {
        mana.Decrease(_currentSkill.manaCost);
        if (PlayerMovement.instance.facingDirection == 1) ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setSkill(_currentSkill, new Vector2(transform.position.x + 1f, transform.position.y), PlayerMovement.instance.facingDirection);
        else ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setSkill(_currentSkill, new Vector2(transform.position.x - 1f, transform.position.y), PlayerMovement.instance.facingDirection);
    }
}
