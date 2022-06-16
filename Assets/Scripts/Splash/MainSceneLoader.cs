using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
    }
}
