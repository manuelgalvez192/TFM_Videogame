using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PlayerDie : MonoBehaviour
{
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
    
    [SerializeField] public static bool canControl;
    [SerializeField] private Animator animator;
    
    public delegate void DisableActions();
    public static DisableActions disableActions;
    
    void Start()
    {
        postProcess.profile.TryGetSettings(out aberration);
        postProcess.profile.TryGetSettings(out grading);
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
        
        canControl = false;
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
}
