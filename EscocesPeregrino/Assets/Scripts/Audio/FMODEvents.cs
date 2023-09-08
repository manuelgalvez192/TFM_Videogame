using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerHit { get; private set; }
    [field: SerializeField] public EventReference playerNoHit { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference playerLand { get; private set; }
    [field: SerializeField] public EventReference playerSuperPunch { get; private set; }
    [field: SerializeField] public EventReference playerBlock { get; private set; }
    [field: SerializeField] public EventReference playerDeath { get; private set; }


    [field: Header("Enemies SFX")]
    [field: SerializeField] public EventReference enemieDeath { get; private set; }
    [field: SerializeField] public EventReference enemiePunch { get; private set; }
    [field: SerializeField] public EventReference enemieSword { get; private set; }



    [field: Header ("Menu SFX")]
    [field: SerializeField] public EventReference menuClick { get; private set; }
    [field: SerializeField] public EventReference menuMusic { get; private set; }

    [field: Header("Map SFX")]
    [field: SerializeField] public EventReference laser { get; private set; }
    [field: SerializeField] public EventReference lvl1Music { get; private set; }
    [field: SerializeField] public EventReference lvl2Music { get; private set; }
    [field: SerializeField] public EventReference fanfareMusic { get; private set; }
    public static FMODEvents instance { get; private set; }



    


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("FOund more than one FMOD Events instance in the scene");
        }
        instance= this;
    }
}
