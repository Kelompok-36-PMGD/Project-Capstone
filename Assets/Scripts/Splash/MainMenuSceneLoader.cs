using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
