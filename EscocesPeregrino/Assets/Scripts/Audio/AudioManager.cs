using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;



public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    private Dictionary<EventReference, EventInstance> musicInstances = new Dictionary<EventReference, EventInstance>();

    EventInstance musicEventInstance;
  
    private void Awake()
    {
        if(instance != null)
        {
            //Debug.LogError("Found more than one Audio Manager in the scene");
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar de escena
        }
        instance = this;
    }

 
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
        
    }

    public void PlayMusic(EventReference sound)
    {
        //StopMusic();

        if (musicInstances.ContainsKey(sound))
        {
            // La instancia ya existe, no la vuelvas a crear
            return;
        }

        musicEventInstance = RuntimeManager.CreateInstance(sound);
        musicEventInstance.start();
        musicInstances[sound] = musicEventInstance;

    }

    public void StopMusic(EventReference sound)
    {
         print("Trying to stop music for " + sound);
            print("Stopping music for " + sound);
                musicInstances[sound] = musicEventInstance;
                musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                musicEventInstance.release();
                musicInstances.Remove(sound);


  


    }


}
