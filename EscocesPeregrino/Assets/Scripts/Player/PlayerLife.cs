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
    
    

    void Start()
    {
        playerLifeSlider.maxValue = maxLife;
        playerLifeSlider.value = maxLife;
        currentLife = maxLife;
        lifeText.text = "x" + currentLife.ToString();
    }
    public void GetDamage(float damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            EnemyAI.canFollow = false;
            currentLife = 0;
            //die(); AQUI EL MORIR
        }
        else
            animator.SetTrigger("takeDamage");

        playerLifeSlider.value = currentLife;
        lifeText.text = "x" + currentLife.ToString();
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
    }
}
