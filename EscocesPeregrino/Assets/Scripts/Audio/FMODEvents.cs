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

    [field: Header ("Menu SFX")]
    [field: SerializeField] public EventReference menuClick { get; private set; }
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
