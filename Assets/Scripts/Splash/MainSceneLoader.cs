using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    public string nextStage;
    public bool increaseLevelInManager;

    void OnEnable()
    {
        if (increaseLevelInManager) GameManager.instance.currentScene += 1;
        SceneManager.LoadScene(nextStage, LoadSceneMode.Single);
    }
}
