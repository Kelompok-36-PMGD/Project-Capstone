using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]public GameObject pausePanel;
    [SerializeField]public GameObject deathPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy) PauseGame();
            else ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        PlayerSound.instance.pauseButtonSound();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        PlayerSound.instance.pauseButtonSound();
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
        PlayerSound.instance.pauseButtonSound();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        PlayerSound.instance.ClickButtonSound();
        GameManager.instance.LoadDataFromGameManager();
        InventorySystem.instance.ClearAllInventory();
        SceneManager.LoadScene(GameManager.instance.currentScene, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        Destroy(GameManager.instance.gameObject);
        PlayerSound.instance.ClickButtonSound();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
