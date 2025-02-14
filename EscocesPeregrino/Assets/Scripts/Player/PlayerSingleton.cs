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
    public StrongAttack playerStrongAttack;
    public PlayerMovement playerMovement;
    public PlayerBlock playerBlock;
    public Animator playerAnimator;
    public bool isSpecialAttack = false;
    [SerializeField] ParticleSystem rayParticles;

    void Awake()
    {
        if (instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public void PlayRayParticles()
    {
        rayParticles.Play();
    }

    public void SetPlayerAllowedToMove(bool result)
    {
        playerMovement.allowedToRun = result;
        playerBasicAttack.allowedToRun = result;
        playerStrongAttack.allowedToRun = result;
        playerBlock.allowedToRun = result;
    }
}


