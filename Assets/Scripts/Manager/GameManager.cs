using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Coin")]
    public Health coinScriptable;
    [Header("Player")]
    public List<SkillList> skillList;
    public Health lifeScriptable;
    public Health manaScriptable;
    public List<GameObject> inventoryItems;
    [Header("Scene")]
    public int currentScene;
    public int shopScene;
    public int lastScene;

    public bool inShop = false;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// FOR TESTING ONLY
    /// </summary>
    private void Update()
    {
        //TESTING
        //if (Input.GetKeyDown(KeyCode.P)) ToShopScene();
    }

    public void SaveDataToGameManager()
    {
        InventorySystem.instance.ClearInventoryOnNextLevel();
        inventoryItems = new List<GameObject>();
        skillList = PlayerController.instance.GetComponent<Skills>()._skillList;
        lifeScriptable = PlayerController.instance.GetComponent<Life>().lifeScriptable;
        manaScriptable = PlayerController.instance.GetComponent<Mana>().mana;
        foreach(GameObject go in InventorySystem.instance.items)
        {
            inventoryItems.Add(go);
        }
    }

    public void LoadDataFromGameManager()
    {
        if (!inShop)
        {
            PlayerController.instance.GetComponent<Skills>()._skillList = skillList;
            PlayerController.instance.GetComponent<Life>().lifeScriptable = lifeScriptable;
            PlayerController.instance.GetComponent<Mana>().mana = manaScriptable;
            InventorySystem.instance.items = inventoryItems;
            Debug.Log("Data loaded from GameManager!");
        }
    }

    public void ToShopScene()
    {
        SaveDataToGameManager();
        inShop = true;
        SceneManager.LoadScene(shopScene);
    }

    public void NextLevel()
    {
        if(currentScene == lastScene)
        {
            Debug.Log("This is the last scene, cant proceed to next level..");
            return;
        }
        currentScene += 1;
        int next = currentScene;
        SceneManager.LoadScene(next);
    }

}
