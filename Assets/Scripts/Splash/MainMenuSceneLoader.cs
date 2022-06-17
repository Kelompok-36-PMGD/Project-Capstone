using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.instance.lifeScriptable.resetDefault();
        GameManager.instance.coinScriptable.resetDefault();
        GameManager.instance.manaScriptable.resetDefault();
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
