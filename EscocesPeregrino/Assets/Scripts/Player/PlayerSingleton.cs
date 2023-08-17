using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    static PlayerSingleton Instance;
    public static PlayerSingleton instance
    { get { return Instance; } }


    [Header("Components")]
    public PlayerLife playerLife;

    void Awake()
    {
        if (instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Update()
    {
        
    }
}
