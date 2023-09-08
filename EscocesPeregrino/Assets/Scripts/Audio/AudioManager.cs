using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;

    private EventInstance menuMusicInstance;
    private EventInstance lvl1MusicInstance;
    private EventInstance lvl2MusicInstance;

    public bool isMenuM=false;
    public bool isLvl1M = false;
    public bool isLvl2M = false;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        //menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/MusicMainMenu");
        //lvl1MusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/MusicLvl1");
        //lvl2MusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/MusicLvl2");
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
        
    }

    public void PlayMusic(EventReference sound, Vector3 worldPos)
    {
        StopMusic();

        if (isMenuM)
        {
            menuMusicInstance = RuntimeManager.CreateInstance(sound);
            menuMusicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
            menuMusicInstance.start();
        }
        else if (isLvl1M)
        {
            lvl1MusicInstance = RuntimeManager.CreateInstance(sound);
            lvl1MusicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
            lvl1MusicInstance.start();
        }
        else if (isLvl2M)
        {
            lvl2MusicInstance = RuntimeManager.CreateInstance(sound);
            lvl2MusicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
            lvl2MusicInstance.start();
        }
    }

    public void StopMusic()
    {
        if(isMenuM)
        {
            menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            menuMusicInstance.release();
        }

        if (lvl1MusicInstance.isValid())
        {
            menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            lvl1MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            lvl1MusicInstance.release();
        }

        if (lvl2MusicInstance.isValid())
        {
            lvl2MusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            lvl2MusicInstance.release();
        }

    }


}
