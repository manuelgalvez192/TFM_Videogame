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
    [SerializeField] float maxLife = 10;
    float currentLife;
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

    void Start()
    {
        playerLifeSlider.maxValue = maxLife;
        playerLifeSlider.value = maxLife;
        currentLife = maxLife;
        postProcess.profile.TryGetSettings(out aberration);
        postProcess.profile.TryGetSettings(out grading);
    }

    private void Update()
    {
       // aberration.intensity.value = gAmount;
       // grading.temperature .value= fgradingAmount;
       // grading.tint .value= fgradingAmount;

        if (Input.GetKeyDown(KeyCode.Y))
            GetDamage(11);
    }

    public void GetDamage(float damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
            Die();
        playerLifeSlider.value = currentLife;
    }
    
    public void GetHeal(float heal)
    {
        currentLife += heal;
        if (currentLife >= maxLife)
            currentLife = maxLife;
        playerLifeSlider.value = currentLife;
    }
    
    void Die()
    {
        StartCoroutine(AberrationUpdate());
        StartCoroutine(ColorGradientUpdate());
        StartCoroutine(AnmAndChangeScene());
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
        //animator.SetTrigger("die");
        yield return new WaitForSecondsRealtime(4);
       // UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyPunchHB")
        {
            animator.SetTrigger("takeDamage");
            GetDamage(enemyDamage.enemyDamage);
            StartCoroutine(CanControl());
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "EnemyPunchHB")
        {
            
        }
    }

    private IEnumerator CanControl()
    {
        print("entra");
        canControl.canControl = false;
        yield return new WaitForSeconds(2);
        canControl.canControl = true;
    }
}
