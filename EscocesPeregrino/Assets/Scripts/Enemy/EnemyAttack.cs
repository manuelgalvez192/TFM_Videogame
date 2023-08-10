using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyAI enemyAi;

    private bool isOnRange = false;
    private bool atacking = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("isWalking", false);
            enemyAi.canFollow = false;
            animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (transform.parent.gameObject.activeInHierarchy)
            {
                StartCoroutine(AttackToIdle());
            }
        }
    }

    private IEnumerator AttackToIdle()
    {
        yield return new WaitForSeconds(1);
        
        enemyAi.canFollow = true;
        
        animator.SetBool("isAttacking", false);
        atacking = false;
        yield break;
    }

}
