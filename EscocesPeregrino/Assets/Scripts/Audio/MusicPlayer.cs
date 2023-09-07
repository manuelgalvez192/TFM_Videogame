using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public bool isMenuMusic;
    public bool islvl1Music;
    public bool islvl2Music;

    // Start is called before the first frame update
    void Start()
    {
        if (isMenuMusic)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.menuMusic, this.transform.position);
        }

        if (islvl1Music)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.lvl1Music, this.transform.position);
        }

        if (islvl2Music)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.lvl2Music, this.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(!islvl1Music)
       {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.menuMusic, this.transform.position);
       }
       if (!isMenuMusic)
       {

       }
       if(!islvl2Music)
       {

       }
    }
}
