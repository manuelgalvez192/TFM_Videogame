using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
#region Singleton
    static PostProcessingManager Instance;
    public static PostProcessingManager instance 
    { 
        get { return Instance; }
    }
        
    #endregion
    [SerializeField] PostProcessVolume postVolume;
    ChromaticAberration cAberration;
    ColorGrading cGrading;
    [Header("On DieEffects")]
    [SerializeField]float dieGradingMultiplier;
    [SerializeField]float dieAberrationMultiplier;
    [SerializeField] AnimationCurve dieAberrationCurve;
    [SerializeField] AnimationCurve diegradingCurve;
    Animator playerAnimator;
    

    void Awake()
    {
        //Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        //PostProcessing
        postVolume.profile.TryGetSettings<ChromaticAberration>(out cAberration);
        postVolume.profile.TryGetSettings<ColorGrading>(out cGrading);
    }
    private void Start()
    {
        playerAnimator = PlayerSingleton.instance.playerAnimator;
    }
    #region OnPlayerDie
    public void OnPlayerDie()
    {
        StartCoroutine(AberrationUpdate());
        StartCoroutine(ColorGradientUpdate());
        //StartCoroutine(AnmAndChangeScene());
        playerAnimator.SetTrigger("die");
        //StartCoroutine(TakeOutControl());
    }
    private IEnumerator TakeOutControl()
    {
        yield return new WaitForSeconds(1.1f);

        //canControl = false;
        //disableActions?.Invoke();
    }

    IEnumerator AberrationUpdate()
    {
        float aberrationX = 0;
        float aberrationY = 0;
        while (true)
        {
            aberrationX += Time.deltaTime;
            if (aberrationX > dieAberrationCurve[dieAberrationCurve.length - 1].time)
                aberrationX = dieAberrationCurve[1].time;

            aberrationY = dieAberrationCurve.Evaluate(aberrationX);
            cAberration.intensity.value = aberrationY * dieAberrationMultiplier;
            yield return null;
        }

        yield break;
    }

    IEnumerator ColorGradientUpdate()
    {
        float gradientX = 0;
        float gradientY = 0;
        while (gradientX < diegradingCurve[diegradingCurve.length - 1].time)
        {
            gradientX += Time.deltaTime * 0.5f;
            gradientY = diegradingCurve.Evaluate(gradientX);
            cGrading.temperature.value = gradientY * dieGradingMultiplier;
            cGrading.tint.value = gradientY * dieGradingMultiplier;
            yield return null;
        }

        yield break;
    }

    IEnumerator AnmAndChangeScene()
    {

        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    #endregion

    public void ResetComponent()
    {
        StopAllCoroutines();
        StartCoroutine(ResetColorGrading());
        StartCoroutine(ResetChromaticAberration());
    }
    IEnumerator ResetColorGrading()
    {
        float gradientX = diegradingCurve[diegradingCurve.length - 1].time;
        float gradientY = 0;
        while (gradientX > 0)
        {
            gradientX -= Time.deltaTime * 0.5f;
            gradientY = diegradingCurve.Evaluate(gradientX);
            cGrading.temperature.value = gradientY * dieGradingMultiplier;
            cGrading.tint.value = gradientY * dieGradingMultiplier;
            yield return null;
        }
        yield break;
    }
    IEnumerator ResetChromaticAberration()
    {
        float aberrationX = dieAberrationCurve[dieAberrationCurve.length - 1].time;
        float aberrationY = 0;
        bool canRun = true;
        while (canRun)
        {
            aberrationX -= Time.deltaTime;
            if (aberrationX < 0)
                canRun = false; 

            aberrationY = dieAberrationCurve.Evaluate(aberrationX);
            cAberration.intensity.value = aberrationY * dieAberrationMultiplier;
            yield return null;
        }
        yield break;
    }


}
