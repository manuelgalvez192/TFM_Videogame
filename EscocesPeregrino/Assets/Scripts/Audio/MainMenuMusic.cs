using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MainMenuMusic : MonoBehaviour
{
    private EventInstance musicEventInstance;

    public string menuSceneName = "MainMenu";

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        musicEventInstance = RuntimeManager.CreateInstance("event:/MusicLvl1");
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayMusic()
    {
        musicEventInstance.start();
    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicEventInstance.release();
    }
}
