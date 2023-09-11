using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyAI enemyAi;
    [SerializeField] private EnemyLife enemyLife;
    [SerializeField] private GameObject enemyPunchHB;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private PlayerLife currentLife;
    public float enemyDamage;

    private bool isOnRange = false;
    private bool atacking = false;

    private float timetoSoundAttack=0;

    private void Start()
    {
        currentLife = PlayerSingleton.instance.playerLife;
        //PlayerLife.disableActions += DisableEnemyActions;
    }

    private void Update()
    {
        if(atacking)
        {

            timetoSoundAttack -= Time.deltaTime;
            if(timetoSoundAttack<=0)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.enemiePunch, this.transform.position);
                timetoSoundAttack = 1.2f;
            }

        }
        else
        {

        }
        
    }

    private void DisableEnemyActions()
    {
        enemyAi.canFollow = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", false);
        capsule.enabled = false;
    }
    public void EnablaAttackOnResucite()
    {
        capsule.enabled = true;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", false);
    }

    private void OnDisable()
    {
        //PlayerLife.disableActions -= DisableEnemyActions;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            animator.SetBool("isWalking", false);
            enemyAi.canFollow = false;
            animator.SetBool("isAttacking", true);
            enemyPunchHB.SetActive(true);
            atacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (transform.parent.gameObject.activeInHierarchy)
            {
                StartCoroutine(AttackToIdle());
                enemyPunchHB.SetActive(false);
            }
        }
    }

    private IEnumerator AttackToIdle()
    {
        if (currentLife.currentLife > 0 && enemyLife.isAlive)
        {
            yield return new WaitForSeconds(1);
        
            enemyAi.canFollow = true;
            print("entraaaaaaaaaaaaaaaaaaaaaa");
            animator.SetBool("isAttacking", false);
            atacking = false;
            yield break;
        }
    }
    public void StopBehavior()
    {
        DisableEnemyActions();
    }
}
