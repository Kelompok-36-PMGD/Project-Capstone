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
    }

    public void StartAssignSkill()
    {
        if (_skillList.Count != 0) _currentSkill = _skillList[0];
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        skillTimer += Time.deltaTime;
        if (PlayerMovement.instance.isClimbing) return; //Cant shoot while climbing

        if (Input.GetKeyDown(KeyCode.C) && _currentSkill.attackType != AttackType.NORMAL)
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && _skillList[0] != null) _currentSkill = _skillList[0];
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _skillList[1] != null) _currentSkill = _skillList[1];
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _skillList[2] != null) _currentSkill = _skillList[2];
    }

    void SkillDelay()
    {
        mana.Decrease(_currentSkill.manaCost);
        if (PlayerMovement.instance.facingDirection == 1) ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setSkill(_currentSkill, new Vector2(transform.position.x + 1f, transform.position.y), PlayerMovement.instance.facingDirection);
        else ObjectPool.instance.requestObject(PoolObjectType.SkillObject).gameObject.GetComponent<SkillObject>().setSkill(_currentSkill, new Vector2(transform.position.x - 1f, transform.position.y), PlayerMovement.instance.facingDirection);
    }

    public void AddSkill(SkillList skill)
    {
        _skillList.Add(skill);
        _currentSkill = skill;
    }

    /// <summary>
    /// This add a skill to the player gameObject through PlayerController scripts
    /// </summary>
    /// <param name="skill">Skill to be added</param>
    public void AddSkillToPlayer(SkillList skill)
    {
        PlayerController.instance.gameObject.GetComponent<Skills>().AddSkill(skill);
    }

    public bool Contains(SkillList skill)
    {
        if (_skillList.Contains(skill)) return true;
        else return false;
    }
}
