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

    public MainMenuMusic mainMenuMusic;

    public static SceneManagerMusic instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject); // Destruye duplicados
        }
    }

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
        // Aqu� det�n la m�sica actual seg�n la escena cargada
        StopCurrentMusic(scene.name);

        // Agrega l�gica para iniciar la nueva m�sica seg�n la escena, si es necesario
        PlayNewMusic(scene.name);
    }

    private void StopCurrentMusic(string sceneName)
    {
        AudioManager audioManager = AudioManager.instance;
        if (audioManager != null)
        {
            // Implementa la l�gica para detener la m�sica actual seg�n la escena
            if (sceneName != menuSceneName)
            {
                
                //audioManager.StopMusic(FMODEvents.instance.menuMusic);
              
            }
            else if (sceneName != lvl1SceneName)
            {
              
                
            }
            else if (sceneName != lvl2SceneName)
            {
                
               
            }
            // Agrega m�s condiciones seg�n tus necesidades
        }
    }

    private void PlayNewMusic(string sceneName)
    {
        AudioManager audioManager = AudioManager.instance;
        if (sceneName == menuSceneName)
        {
            mainMenuMusic.PlayMusic();
            //audioManager.PlayMusic(FMODEvents.instance.menuMusic);
        }
        else if (sceneName == lvl1SceneName)
        {
           
           
        }
        else if (sceneName == lvl2SceneName)
        {
          
            
        }
    }
}
