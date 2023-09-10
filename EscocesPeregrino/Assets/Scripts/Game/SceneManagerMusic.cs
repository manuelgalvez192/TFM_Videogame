using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneManagerMusic : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public string lvl1SceneName = "Manu";
    public string lvl2SceneName = "Level2";

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aquí detén la música actual según la escena cargada
        StopCurrentMusic(scene.name);

        // Agrega lógica para iniciar la nueva música según la escena, si es necesario
        PlayNewMusic(scene.name);
    }

    private void StopCurrentMusic(string sceneName)
    {
        AudioManager audioManager = AudioManager.instance;
        if (audioManager != null)
        {
            // Implementa la lógica para detener la música actual según la escena
            if (sceneName != menuSceneName)
            {
               
                audioManager.StopMusic(FMODEvents.instance.menuMusic);
              
            }
            else if (sceneName != lvl1SceneName)
            {
              
                audioManager.StopMusic(FMODEvents.instance.lvl1Music);
                
            }
            else if (sceneName != lvl2SceneName)
            {
                audioManager.StopMusic(FMODEvents.instance.lvl2Music);
               
            }
            // Agrega más condiciones según tus necesidades
        }
    }

    private void PlayNewMusic(string sceneName)
    {
        AudioManager audioManager = AudioManager.instance;
        if (sceneName ==menuSceneName)
        {
        
            audioManager.PlayMusic(FMODEvents.instance.menuMusic);
        }
        else if (sceneName == lvl1SceneName)
        {
            
            audioManager.PlayMusic(FMODEvents.instance.lvl1Music);
        }
        else if (sceneName == lvl2SceneName)
        {
          
            audioManager.PlayMusic(FMODEvents.instance.lvl2Music);
        }
    }
}
