using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void OnPauseGame()//gamepad press button
    {
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = pauseMenu.activeInHierarchy ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}
