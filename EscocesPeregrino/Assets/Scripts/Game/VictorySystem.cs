using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VictorySystem : MonoBehaviour
{
    [SerializeField]PostProcessVolume postPorcesObject;
    [Header("Vinyeta")]
    [SerializeField] float vignetteSpeed;
    [SerializeField] AnimationCurve curveVignetteSpeed;
    Vignette vignete;

    [Header("UI")]
    [SerializeField] Transform canvas;
    [SerializeField] float scaleSpeed;
    [SerializeField] float scaleMultiplier;

    [SerializeField] AnimationCurve curveScaleSpeed;
    [SerializeField] private PlayerMovement canControl;
    [SerializeField] private Animator _animator;

    [SerializeField] bool needToKillAllEnemies = false;
    bool allEnemiesDied = false;
    int totalEnemies;
    int currentEnemies =0;

    private MainMenuMusic menuMusic;

    
    void Start()
    {
        postPorcesObject.profile.TryGetSettings(out vignete);

        if (!needToKillAllEnemies)
        {
            allEnemiesDied = true;
            return;

        }

        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("LifeComponent");
        totalEnemies = enemyArray.Length - 1;
    }
    IEnumerator Win()
    {
       
        float x = 0;
        float y = 0;
        bool CanRun = true;
        while (CanRun)
        {
            x += Time.deltaTime*vignetteSpeed;
            y = curveVignetteSpeed.Evaluate(x);
            vignete.intensity.value = y;
            CanRun = vignete.intensity.value <= 0.570755;
            yield return null;
        }
        canvas.gameObject.SetActive(true);
        x = 0;
        y = 0;
        do
        {
            x += Time.deltaTime * scaleSpeed;
            y = curveScaleSpeed.Evaluate(x);
            y *= scaleMultiplier;

            canvas.localScale = new Vector3(y, y, y);

            CanRun = x < curveScaleSpeed[curveScaleSpeed.length-1].time;

            yield return null;
        } while (CanRun);
        
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        yield break;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!allEnemiesDied)
                return;
            canControl.canControl = false;
            PlayerSingleton.instance.playerMovement.StopMovement();
            _animator.SetTrigger("victory");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.fanfareMusic, this.transform.position);
            StartCoroutine("Win");
            StartCoroutine("SetMusicVolume");
        }
    }
    
    public void OnEnemyDie()
    {
        if (!needToKillAllEnemies)
            return;
        currentEnemies++;
        if(currentEnemies>= totalEnemies)
        {
            allEnemiesDied = true;
        }
    }

    private  IEnumerator SetMusicVolume()
    {
        menuMusic.SetVolume(0.1f);

        yield return new WaitForSeconds(10f);

        menuMusic.SetVolume(1);

        yield return null;

    }
        

}
