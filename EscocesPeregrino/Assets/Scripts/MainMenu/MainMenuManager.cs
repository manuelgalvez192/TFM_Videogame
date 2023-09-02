using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuManager : MonoBehaviour
{
    
    void Start()
    {
    }
    public void PlayGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuClick, this.transform.position);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void DoSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuClick, this.transform.position);
    }
}
