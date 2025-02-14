using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelPrincipal;
    [SerializeField] private GameObject PanelLoader;
    
    void Start()
    {
        //AdsManager.instance.ThrowBannerAdd();
    }
    public void PlayGame(int NumberScene)
    {
        StartCoroutine(PlaySoundBeforeStart(NumberScene));
    }
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator PlaySoundBeforeStart(int NumberScene)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuClick, this.transform.position);
        yield return new WaitForSeconds(0.1f);
        AdsManager.instance.CloseBannerAdd();
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        StartCoroutine(LoadAsync(NumberScene));
    }
    
    IEnumerator LoadAsync(int NumberScene)
    {
        PanelPrincipal.SetActive(false);
        PanelLoader.SetActive(true);
        
        yield return new WaitForSeconds(2);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(NumberScene);

    }

    public void soundClick()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuClick, this.transform.position);
    }
}
