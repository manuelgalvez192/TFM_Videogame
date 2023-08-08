using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            print("soy player");
            
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
        }
        else
        {
            print("no soy player");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("isAttacking", false);
    }
}
