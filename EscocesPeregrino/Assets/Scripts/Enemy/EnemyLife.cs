using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private GameObject selfEnemy;
    [SerializeField] private float maxLife;
    [SerializeField] private EnemyAI enemyAi;
    [SerializeField] private PlayerBasicAtack playerDamage;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject collision;
    [SerializeField] private GameObject healDrop;
    [SerializeField] private Transform particlePos;

    private float currentLife;
    private int numAnimation;
    private bool isAlive = true;
    Random rnd = new Random();

    private void Start()
    {
        currentLife = maxLife;
        playerDamage = PlayerSingleton.instance.playerBasicAttack;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerPunchHB")
        {
            if (isAlive)
            {
                numAnimation = rnd.Next(1, 3);
                currentLife -= playerDamage.playerDamage;
                if (currentLife <= 0)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.enemieDeath, this.transform.position);
                    isAlive = false;
                    collision.SetActive(false);
                    StartCoroutine(EnemyDeadAnimation());
                }

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
                ThrowDamageParticle();
            }
        }
    }
    void ThrowDamageParticle()
    {
        
            int rand = UnityEngine.Random.Range(0, 2);
            if (PlayerSingleton.instance.isSpecialAttack)
            {
                ParticleSystemManager.instance.ThrowParticleSystem(rand == 0 ? "Boom" : "Fire", particlePos);
            }
            else
                ParticleSystemManager.instance.ThrowParticleSystem(rand == 0 ? "BasicHit" : "RandomText", particlePos);
        ParticleSystemManager.instance.ThrowParticleSystem("Blood", particlePos);
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
    
    private IEnumerator EnemyDeadAnimation()
    {
        animator.SetTrigger("isDead");
        EnemyAI.canFollow = false;
        yield return new WaitForSeconds(2.5f);
        ParticleSystemManager.instance.ThrowParticleSystem("Die", particlePos);

        int randHealDrop = UnityEngine.Random.Range(0, 3);
        if(randHealDrop>1)
        {
            GameObject newDrop = Instantiate(healDrop, transform.position,transform.rotation);//TO DO:: Si se hace pool camboiar el spawn
            newDrop.GetComponent<PickeableObject>().OnDropObject();
        }
        yield return new WaitForSeconds(2.5f);
        selfEnemy.SetActive(false);
    }
}
