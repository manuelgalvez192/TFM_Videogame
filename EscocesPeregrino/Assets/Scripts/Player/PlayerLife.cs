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
    [SerializeField] private EnemyAttack enemyDamage;

    [SerializeField] private Animator animator;
    [SerializeField] ParticleSystem electricParticles;

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
        if(!isBlocking)
        {
            currentLife -= damage;
            if (currentLife <= 0)
            {
                currentLife = 0;
                PostProcessingManager.instance.OnPlayerDie();
                EnemyAI.canFollow = false;
                //die(); AQUI EL MORIR
                GE_onPlayerDieEvent.Raise();
                PlayerSingleton.instance.playerMovement.StopMovement();
                if(!hasDied)
                {
                    hasDied = true;
                    SecondChancePanel.SetActive(true);
                }
                else 
                {
                    StartCoroutine(GoMenuAfterDie());
                }
            }

            animator.SetTrigger("takeDamage");
            playerLifeSlider.value = currentLife;
            lifeText.text = "x" + currentLife.ToString();
        }
       
    }
    
    public void GetHeal(float heal)
    {
        currentLife += heal;
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
    private void OnTriggerEnter2D(Collider2D other)
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
    }
    IEnumerator GoMenuAfterDie()
    {
        yield return new WaitForSeconds(4);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        yield break;
    }
    public void ResetComponent()
    {
        StopAllCoroutines();
        Start();
        PlayerSingleton.instance.playerMovement.canControl = true;
    }
}
