using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        //AdsManager.instance.ThrowBannerAdd();
    }
    public void PlayGame()
    {
        AdsManager.instance.CloseBannerAdd();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
