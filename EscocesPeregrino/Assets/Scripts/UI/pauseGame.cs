using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class pauseGame : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private float resumeButtonScaleX;
    [SerializeField] private float resumeButtonScaleY;
    [SerializeField] private float exitButtonScaleX;
    [SerializeField] private float exitButtonScaleY;
    
    [SerializeField] private GameObject PanelLoader;

    private short positionMenu = 1;
    private bool paused = false;

    void Update()
    {
        if (pauseMenu != null)
        {
            if (InputsGameManager.instance.PauseButtonDown)
            {
                PauseGame();
            }
        }

        if (exitButton != null && resumeButton != null)
        {
            if (InputsGameManager.instance.MoveMenuDownDown || InputsGameManager.instance.MoveMenuUpDown)
            {
                OnChangeMenuOption();
            }
        }
        
        if (paused && InputsGameManager.instance.AttackButtonDown && (positionMenu == 1))
        {
            PauseGame();
        }
    
        if (paused && InputsGameManager.instance.AttackButtonDown && (positionMenu == 2))
        {
            ExitMainMenu();
        }
        
        
    }

    public void PauseGame()
    {
        Time.timeScale = pauseMenu.activeInHierarchy ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        paused = !paused;
        PlayerSingleton.instance.SetPlayerAllowedToMove(!paused);
        
        //initial selected button resume
        resumeButton.transform.localScale = new Vector3(resumeButtonScaleX + 0.1f,
            resumeButtonScaleY + 0.1f, 1);
       
        positionMenu = 1;
        
        //reset exit button scale
        exitButton.transform.localScale = new Vector3(exitButtonScaleX,
            exitButtonScaleY, 1);
    }

    private void OnChangeMenuOption()//gamepad press button dpad down/up
    {
        if (positionMenu == 1)
        {
            positionMenu = 2;
            
            exitButton.transform.localScale = new Vector3(exitButtonScaleX + 0.1f,
                exitButtonScaleY + 0.1f, 1);
            
            resumeButton.transform.localScale = new Vector3(resumeButtonScaleX,
                resumeButtonScaleY, 1);
        }
        else
        {
            positionMenu = 1;
            exitButton.transform.localScale = new Vector3(exitButtonScaleX,
                exitButtonScaleY, 1);
            
            resumeButton.transform.localScale = new Vector3(resumeButtonScaleX + 0.1f,
                resumeButtonScaleY + 0.1f, 1);
        }
    }

    public void ExitMainMenu()
    {
        StartCoroutine(LoadAsync());
        
        Time.timeScale = pauseMenu.activeInHierarchy ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        paused = !paused;
    }
    
    IEnumerator LoadAsync()
    {
        PanelLoader.SetActive(true);
        
        yield return new WaitForSeconds(2);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

    }
}
