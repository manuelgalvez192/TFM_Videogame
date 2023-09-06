using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAtack : MonoBehaviour
{

    [SerializeField] private GameObject hitbox;
    [SerializeField] private Animator animator;
    public float playerDamage;
    
    private int comboCount;
    private bool atacking;
    private bool enemyHit;
    
    public delegate bool CanMove(bool value);
    public static CanMove canMove;

    void Start()
    {
        atacking = false;
        enemyHit = false;
        comboCount = 0;
    }

    void Update()
    {
        if(InputsGameManager.instance.AttackButtonDown)
        {
            Combo();
        }
    }

    public void Combo()
    {
        if (!atacking)
        {
            if(PlayerSingleton.instance.playerMovement.isGrounded)
            {
                PlayerSingleton.instance.isSpecialAttack = false;//cosas de particulas

                canMove?.Invoke(false);
                hitbox.SetActive(true);
                atacking = true;
                animator.SetTrigger("" + comboCount);
                if(enemyHit)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHit, this.transform.position);
                }
                else if(!enemyHit)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerNoHit, this.transform.position);
                }

            }
            else
            {
                //patada voladora siuuuuuuuu
            }
        }
    }
    
    public void StartCombo()
    {
        atacking = false;
        
        if (comboCount < 3)
        {
            comboCount++;
        }
    }

    public void FinishCombo()
    {
        comboCount = 0;
        canMove?.Invoke(true);
        atacking = false;
        hitbox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            enemyHit = true;
        }
    }
}
