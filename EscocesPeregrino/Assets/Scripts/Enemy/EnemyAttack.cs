using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isOnRange = false;
    public delegate bool CanMove(bool value);
    public static CanMove canMove;
    private bool atacking = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("isWalking", false);
            canMove?.Invoke(false);
            animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("isAttacking", false);
            atacking = false;
        }
    }

}
