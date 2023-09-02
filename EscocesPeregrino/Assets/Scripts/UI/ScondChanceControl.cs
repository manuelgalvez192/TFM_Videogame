using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScondChanceControl : MonoBehaviour
{
    [SerializeField] Image timeCircle;
    [SerializeField] float timeToCkick = 5;
    [SerializeField] GameObject panel;
    IEnumerator currentBehavior;
    [SerializeField] GameEvent onPlayerResucite;
    void OnEnable()
    {
        panel.SetActive(false);
        Invoke("ActivePanel", 1f);
    }

    IEnumerator SecondChangeBehaviour()
    {
        float elapsedTime = 0;
        float currentFillAmount =0;

        while(elapsedTime<timeToCkick)
        {
            currentFillAmount = Mathf.Lerp(0, 1, elapsedTime / timeToCkick);
            elapsedTime+=Time.deltaTime;
            timeCircle.fillAmount = currentFillAmount;
            yield return null;
        }
        GoMenu();
        yield break;
    }
    public void GoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void GetReward()
    {
        StopCoroutine(currentBehavior);
        AdsManager.instance.ThrowVideoRewardAd(onPlayerResucite);
    }
    void ActivePanel()
    {
        panel.SetActive(true);
        currentBehavior = SecondChangeBehaviour();
        StartCoroutine(currentBehavior);
    }

}
