using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private GameObject selfEnemy;
    [SerializeField] private float maxLife;
    [SerializeField] private PlayerBasicAtack playerDamage;
    [SerializeField] private Animator animator;
    
    private float currentLife;
    private int numAnimation;
    Random rnd = new Random();

    private void Start()
    {
        currentLife = maxLife;
    }

    private void Update()
    {
        if (currentLife <= 0)
        {
            selfEnemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerPunchHB")
        {
            numAnimation = rnd.Next(1, 3);
            currentLife -= playerDamage.playerDamage;

            if (numAnimation == 1)
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("takeDamage2", false);
                animator.SetBool("takeDamage1", true);
            }
            if (numAnimation == 2)
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("takeDamage1", false);
                animator.SetBool("takeDamage2", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerPunchHB")
        {
            print("sale");
            animator.SetBool("takeDamage1", false);
            animator.SetBool("takeDamage2", false);
            if (transform.parent.gameObject.activeInHierarchy)
            {
                StartCoroutine(TakeDamageAnim());
            }
        }
    }

    private IEnumerator TakeDamageAnim()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isAttacking", true);
    }
}
