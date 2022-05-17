using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    //Change skills name here
    NORMAL, HARIMAU, KHODAM2, KHODAM3
}

[RequireComponent(typeof(Mana))]
public class Skills : MonoBehaviour
{
    private Mana mana;
    [Header("Skills")]
    public SkillList _currentKhodam;
    public List<SkillList> _khodamList;

    private void Awake()
    {
        mana = GetComponent<Mana>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Usekhodam & damage enemy if on range
            mana.Decrease(_currentKhodam.manaCost);
        }
    }
}
