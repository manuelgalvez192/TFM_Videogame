using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    [SerializeField] private Animator animator;
    [SerializeField] private Image imageCooldown;
    private bool canUseAttack;
    public float strongAttackDamage;
    public PlayerMovement playerMovement;
    public bool allowedToRun = true;
    private void Start()
    {
        imageCooldown.fillAmount = 1;
        canUseAttack = true;
        strongAttackDamage = 10;
    }

    void Update()
    {
        if (!allowedToRun)
            return;
        if (InputsGameManager.instance.DashButtonDown)
        {
            Attack();
        }

        if (imageCooldown.fillAmount < 1)
        {
            imageCooldown.fillAmount += Time.deltaTime / 5;
        }
    }

    private void Attack()
    {
        if (canUseAttack)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerSuperPunch,this.transform.position);
            PlayerSingleton.instance.isSpecialAttack = true;//cosas de particulas
            animator.SetTrigger("strongAttack");
            playerMovement.canControl = false;
            hitbox.SetActive(true);
            imageCooldown.fillAmount = 0;
            StartCoroutine("ResetCooldown");
        }
    }

    public void ResetValues()
    {
        hitbox.SetActive(false);
        playerMovement.canControl = true;
    }

    private IEnumerator ResetCooldown()
    {
        canUseAttack = false;
        yield return new WaitForSeconds(5);
        canUseAttack = true;
    }
}
