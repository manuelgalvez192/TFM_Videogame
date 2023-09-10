using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    private Dictionary<EventReference, EventInstance> musicInstances = new Dictionary<EventReference, EventInstance>();

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
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

        EventInstance soundEventInstance = RuntimeManager.CreateInstance(sound);
        soundEventInstance.start();
        musicInstances[sound] = soundEventInstance;

    }

    public void StopMusic(EventReference sound)
    {
        if (musicInstances.TryGetValue(sound, out EventInstance soundEventInstance))
        {
            if (soundEventInstance.isValid())
            {
                soundEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                soundEventInstance.release();
            }
            musicInstances.Remove(sound);
        }

    }


}
