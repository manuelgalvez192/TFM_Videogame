using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    [SerializeField] private Slider playerLifeSlider;
    [SerializeField] private Text lifeText;
    [SerializeField] float maxLife = 10;
    [NonSerialized] public float currentLife;

    [SerializeField] private Animator animator;
    [SerializeField] Transform particlePos;
    public bool isBlocking;

    [Header("Video for extra life")]
    [SerializeField] GameEvent GE_onPlayerDieEvent;
    bool hasDied = false;
    [SerializeField] GameObject SecondChancePanel;




    void Start()
    {
        playerLifeSlider.maxValue = maxLife;
        playerLifeSlider.value = maxLife;
        currentLife = maxLife;
        lifeText.text = "x" + currentLife.ToString();
    }
    public void GetDamage(float damage)
    {
        PlayerSingleton.instance.playerBasicAttack.FinishCombo();
        StartCoroutine(CanControl());
        if (!isBlocking)
        {
            PlayerSingleton.instance.playerBasicAttack.FinishCombo();
            currentLife -= damage;
            ParticleSystemManager.instance.ThrowParticleSystem("Blood", particlePos);
            if (currentLife <= 0)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, this.transform.position);
                animator.SetTrigger("die");
                currentLife = 0;
                PostProcessingManager.instance.OnPlayerDie();
                
                GE_onPlayerDieEvent.Raise();
                PlayerSingleton.instance.playerMovement.StopMovement();
                if (!hasDied)
                {
                    hasDied = true;
                    SecondChancePanel.SetActive(true);
                }
                else
                {
                    StartCoroutine(GoMenuAfterDie());
                }
            }
            else
            {
                animator.SetTrigger("takeDamage");
            }

            playerLifeSlider.value = currentLife;
            lifeText.text = "x" + currentLife.ToString();
        }
        else if (isBlocking)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerBlock, this.transform.position);
        }

    }

    public void GetHeal(float heal)
    {
        currentLife += heal;
        ParticleSystemManager.instance.ThrowParticleSystem("Heal", particlePos);
        if (currentLife >= maxLife)
            currentLife = maxLife;
        playerLifeSlider.value = currentLife;
        lifeText.text = "x" + currentLife.ToString();
    }

    private IEnumerator CanControl()
    {
        if (currentLife > 0)
        {
            PlayerDie.canControl = false;
            yield return new WaitForSeconds(0.5f);
            PlayerDie.canControl = true;

        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyPunchHB")
        {
            StartCoroutine(CanControl());
            GetDamage(enemyDamage.enemyDamage);
        }
        
        if(other.tag == "Laser")
        {
            if(PlayerSingleton.instance.playerMovement.isGrounded)
            {
                StartCoroutine(CanControl());
                GetDamage(3);
                electricParticles.Play();
            }
        }
    }*/
    IEnumerator GoMenuAfterDie()
    {
        yield return new WaitForSeconds(4);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        yield break;
    }
    public void ResetComponent()
    {
        StopAllCoroutines();
        StartCoroutine(ResetComponentWait());

    }
    IEnumerator ResetComponentWait()
    {
        Start();
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("reset");
        yield return new WaitForSeconds(1.5f);
        PlayerSingleton.instance.playerMovement.canControl = true;
        yield break;
    }
}
