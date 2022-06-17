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

    public TextMesh alert;

    bool isOpened = false;
    bool alertFading = false;

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
        if (alert.color.a != 0 && alertFading) alert.color = new Color(alert.color.r, alert.color.g, alert.color.b, Mathf.Lerp(alert.color.a, 0, 1f * Time.deltaTime));
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
        else
        {
            CancelInvoke("DelayAlertFade");
            Invoke("DelayAlertFade", 3f);
            alertFading = false;
            alert.text = "Remaining enemies : " + (totalEnemies-defeatedEnemies).ToString();
            alert.color = new Color(alert.color.r, alert.color.g, alert.color.b, 1);
            alert.gameObject.SetActive(true);
        }
    }

    void DelayAlertFade()
    {
        alertFading = true;
    }
}
