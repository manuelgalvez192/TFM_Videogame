using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleton : MonoBehaviour
{
    static EnemySingleton Instance;
    public EnemyAttack EnemyAttack;
    public static EnemySingleton instance
    { get { return Instance; } }
    
    void Awake()
    {
        if (instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
