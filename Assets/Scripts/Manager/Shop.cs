using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    public string nextScene;
    public List<ShopSkill> shopList;
    [Header("UI")]
    public Image skillImage;
    public Text nameText;
    public Text priceText;
    public Text descriptionText;
    public GameObject unlockButton;
    public Text coinText;
    public GameObject notEnough;

    List<SkillList> skills;
    SkillList selectedSkill;
    int currentPrice;
    bool bought;

    private void Awake()
    {
        skills = GameManager.instance.skillList;
        coinText.text = GameManager.instance.coinScriptable.value.ToString();
        Skill2Button();
    }

    private void Update()
    {
        Debug.Log(bought);
        if (currentPrice == 0) priceText.text = "Free";
        if (bought) priceText.text = "Bought";
    }

    public void Skill1Button()
    {
        skillImage.sprite = shopList[0].skillImage;
        nameText.text = shopList[0].skillName;
        priceText.text = shopList[0].price.ToString() + " G";
        descriptionText.text = shopList[0].description;
        selectedSkill = shopList[0].skill;
        currentPrice = shopList[0].price;
        if(GameManager.instance.coinScriptable.value > currentPrice)
        {
            notEnough.SetActive(false);
            unlockButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            notEnough.SetActive(true);
            unlockButton.GetComponent<Button>().interactable = false;
        }

        if (skills[0].attackType == selectedSkill.attackType)
        {
            unlockButton.GetComponent<Button>().interactable = false;
            bought = true;
        }
        else
        {
            unlockButton.GetComponent<Button>().interactable = true;
            bought = false;
        }
    }
    public void Skill2Button()
    {
        skillImage.sprite = shopList[1].skillImage;
        nameText.text = shopList[1].skillName;
        priceText.text = shopList[1].price.ToString() + " G";
        currentPrice = shopList[1].price;
        descriptionText.text = shopList[1].description;
        selectedSkill = shopList[1].skill;
        if (GameManager.instance.coinScriptable.value > currentPrice)
        {
            notEnough.SetActive(false);
            unlockButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            notEnough.SetActive(true);
            unlockButton.GetComponent<Button>().interactable = false;
        }

        if (skills.Contains(selectedSkill))
        {
            unlockButton.GetComponent<Button>().interactable = false;
            bought = true;
        }
        else
        {
            unlockButton.GetComponent<Button>().interactable = true;
            bought = false;
        }
    }
    public void Skill3Button()
    {
        skillImage.sprite = shopList[2].skillImage;
        nameText.text = shopList[2].skillName;
        priceText.text = shopList[2].price.ToString() + " G";
        currentPrice = shopList[2].price;
        descriptionText.text = shopList[2].description;
        selectedSkill = shopList[2].skill;
        if (GameManager.instance.coinScriptable.value > currentPrice)
        {
            notEnough.SetActive(false);
            unlockButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            notEnough.SetActive(true);
            unlockButton.GetComponent<Button>().interactable = false;
        }

        if (skills.Contains(selectedSkill))
        {
            unlockButton.GetComponent<Button>().interactable = false;
            bought = true;
        }
        else
        {
            unlockButton.GetComponent<Button>().interactable = true;
            bought = false;
        }
    }

    public void UnlockButton()
    {
        skills.Add(selectedSkill);
        unlockButton.GetComponent<Button>().interactable = false;
        GameManager.instance.coinScriptable.value -= currentPrice;
        coinText.text = GameManager.instance.coinScriptable.value.ToString();
    }

    public void ContinueButton()
    {
        GameManager.instance.inShop = false;
        GameManager.instance.NextLevel();
    }
}

[Serializable]
public struct ShopSkill
{
    public Sprite skillImage;
    public string skillName;
    public int price;
    public string description;
    public SkillList skill;
}