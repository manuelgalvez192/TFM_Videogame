using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    [SerializeField] private Slider playerLifeSlider;
    [SerializeField] private Text lifeText;
    [SerializeField] float maxLife = 10;
    [NonSerialized] public float currentLife;
    [SerializeField] private EnemyAttack enemyDamage;
    [Header("Effects")]
    [SerializeField] PostProcessVolume postProcess;
    
    [Header("Aberrtation")]
    [SerializeField] ChromaticAberration aberration;
    [SerializeField] float aberrationMultiplier;
    [SerializeField] AnimationCurve aberrationCurve;
    
    [Header("ColorGrading")]
    [SerializeField] ColorGrading grading;
    [SerializeField] float gradingMultiply;
    [SerializeField] AnimationCurve gradientCurve;

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement canControl;
    
    public delegate void DisableActions();
    public static DisableActions disableActions;

    void Start()
    {
        playerLifeSlider.maxValue = maxLife;
        playerLifeSlider.value = maxLife;
        currentLife = maxLife;
        lifeText.text = "x" + currentLife.ToString();

        postProcess.profile.TryGetSettings(out aberration);
        postProcess.profile.TryGetSettings(out grading);
    }
    public void GetDamage(float damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            EnemyAI.canFollow = false;
            currentLife = 0;
            Die();
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

    void Die()
    {
        StartCoroutine(AberrationUpdate());
        StartCoroutine(ColorGradientUpdate());
        StartCoroutine(AnmAndChangeScene());
        StartCoroutine(TakeOutControl());
        
    }

    private IEnumerator TakeOutControl()
    {
        yield return new WaitForSeconds(1.1f);
        
        canControl.canControl = false;
        disableActions?.Invoke();
    }
    
    IEnumerator AberrationUpdate()
    {
        float aberrationX = 0;
        float aberrationY = 0;
        while (true)
        {
            aberrationX += Time.deltaTime;
            if (aberrationX > aberrationCurve[aberrationCurve.length - 1].time)
                aberrationX = 0;

            aberrationY = aberrationCurve.Evaluate(aberrationX);
            aberration.intensity.value = aberrationY * aberrationMultiplier;
            yield return null;
        } 

        yield break;
    }
    
    IEnumerator ColorGradientUpdate()
    {
        float gradientX = 0;
        float gradientY =0;
        while(gradientX<gradientCurve[gradientCurve.length-1].time)
        {
            gradientX += Time.deltaTime*0.5f;
            gradientY = gradientCurve.Evaluate(gradientX);
            grading.temperature.value = gradientY * gradingMultiply;
            grading.tint.value = gradientY * gradingMultiply;
            yield return null;
        }

        yield break;
    }
    
    IEnumerator AnmAndChangeScene()
    {
        animator.SetTrigger("die");

        yield return new WaitForSecondsRealtime(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    private IEnumerator CanControl()
    {
        if (currentLife > 0)
        {
            canControl.canControl = false;
            yield return new WaitForSeconds(0.5f);
            canControl.canControl = true;
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
