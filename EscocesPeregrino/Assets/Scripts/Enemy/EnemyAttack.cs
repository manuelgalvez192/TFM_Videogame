using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyAI enemyAi;
    [SerializeField] private GameObject enemyPunchHB;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private PlayerLife currentLife;
    public float enemyDamage;

    private bool isOnRange = false;
    private bool atacking = false;

    private void Start()
    {
        currentLife = PlayerSingleton.instance.playerLife;
        PlayerLife.disableActions += DisableEnemyActions;
    }
    
    private void DisableEnemyActions()
    {
        EnemyAI.canFollow = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", false);
        capsule.enabled = false;
    }

    private void OnDisable()
    {
        PlayerLife.disableActions -= DisableEnemyActions;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("isWalking", false);
            EnemyAI.canFollow = false;
            animator.SetBool("isAttacking", true);
            enemyPunchHB.SetActive(true);
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
        if (currentLife.currentLife > 0)
        {
            yield return new WaitForSeconds(1);
        
            EnemyAI.canFollow = true;
            animator.SetBool("isAttacking", false);
            atacking = false;
            yield break;
        }
    }
}
