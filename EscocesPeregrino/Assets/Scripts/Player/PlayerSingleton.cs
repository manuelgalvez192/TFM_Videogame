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
    public PlayerBasicAtack playerBasicAttack;
    public PlayerMovement playerMovement;
    public Animator playerAnimator;
    public float playerAttackDamage;

    void Awake()
    {
        if (instance == null)
            Instance = this;
        else
            Destroy(this);
        playerAttackDamage = playerBasicAttack.playerDamage;
    }

    void Update()
    {
        
    }
}
