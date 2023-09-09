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
    bool haveConexion = false;
    [SerializeField] Image buttonImg, rewardImage;
    [SerializeField] Text rewardText;
    void OnEnable()
    {
        panel.SetActive(false);
        Invoke("ActivePanel", 1f);

#if UNITY_STANDALONE
        buttonImg.color = new Vector4(buttonImg.color.r, buttonImg.color.g, buttonImg.color.b, buttonImg.color.a / 2);
        rewardImage.color = new Vector4(rewardImage.color.r, rewardImage.color.g, rewardImage.color.b, rewardImage.color.a / 2);
        rewardText.color = new Vector4(rewardText.color.r, rewardText.color.g, rewardText.color.b, rewardText.color.a / 2);
#else
        haveConexion = AdsManager.instance.HaveConexion;
        if(!haveConexion)
        {
            buttonImg.color = new Vector4(buttonImg.color.r, buttonImg.color.g, buttonImg.color.b, buttonImg.color.a / 2);
            rewardImage.color = new Vector4(rewardImage.color.r, rewardImage.color.g, rewardImage.color.b, rewardImage.color.a / 2);
            rewardText.color = new Vector4(rewardText.color.r, rewardText.color.g, rewardText.color.b, rewardText.color.a / 2);

        }
#endif
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
#if UNITY_STANDALONE 
#else
        if (!haveConexion)
            return;
        StopCoroutine(currentBehavior);
        AdsManager.instance.ThrowVideoRewardAd(onPlayerResucite);
        gameObject.SetActive(false);
#endif
    }
    void ActivePanel()
    {
        panel.SetActive(true);
        currentBehavior = SecondChangeBehaviour();
        StartCoroutine(currentBehavior);
    }

}
