using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterFirstRound : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Stage2", LoadSceneMode.Single);
    }
}
