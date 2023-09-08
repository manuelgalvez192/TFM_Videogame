using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    public string menuSceneName = "Menu";
    public string lvl1SceneName = "Manu";
    public string lvl2SceneName = "Level2";
   



    // Start is called before the first frame update
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == menuSceneName )
        {
            AudioManager.instance.PlayMusic(FMODEvents.instance.menuMusic,this.transform.position);
        }

        if (currentSceneName == lvl1SceneName)
        {
            AudioManager.instance.PlayMusic(FMODEvents.instance.lvl1Music, this.transform.position);
        }

        if (currentSceneName == lvl2SceneName)
        {
            AudioManager.instance.PlayMusic(FMODEvents.instance.lvl2Music, this.transform.position);
        }
    }

   
}
