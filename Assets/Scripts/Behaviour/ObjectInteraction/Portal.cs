using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Animator anim;
    public static Portal instance;
    public bool visitShop = true;
    [Header("Enemies")]
    public int totalEnemies;
    public int defeatedEnemies;

    bool isOpened = false;

    private void Awake()
    {
        //Makes sure there are only one portal
        if (instance == null) instance = this;
        else Destroy(gameObject);

        anim = GetComponent<Animator>();
        CheckPortalValid();
    }

    private void Update()
    {
        anim.SetBool("status", isOpened);
    }

    public void MinionDefeated()
    {
        defeatedEnemies += 1;
        CheckPortalValid();
    }

    void CheckPortalValid()
    {
        if (defeatedEnemies == totalEnemies)
        {
            //Add another events when portal is opened heree~~~
            isOpened = true;
        }
    }

    public void InteractPortal()
    {
        if (isOpened)
        {
            if (visitShop)
            {
                //Some Fade Out
                GameManager.instance.ToShopScene();
            }
            else
            {
                GameManager.instance.SaveDataToGameManager();
                GameManager.instance.NextLevel();
            }
        }
    }
}
