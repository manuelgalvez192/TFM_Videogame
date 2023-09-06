using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        AdsManager.instance.ThrowBannerAdd();
    }
    public void PlayGame()
    {
        StartCoroutine(PlaySoundBeforeStart());
    }
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator PlaySoundBeforeStart()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuClick, this.transform.position);
        yield return new WaitForSeconds(0.5f);
        AdsManager.instance.CloseBannerAdd();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void soundClick()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuClick, this.transform.position);
    }
}
