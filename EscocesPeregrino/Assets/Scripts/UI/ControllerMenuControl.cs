using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerMenuControl : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button controlsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button backButton;
    [SerializeField] Button[] changeControlsButton;
    int currentButton = 0;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject[] controllerInfo;

    IEnumerator behaviorCorroutine;
    private void Awake()
    {
        behaviorCorroutine = RunningBehavior();
    }
    public void OnControllerEnter()
    {
        StartCoroutine(behaviorCorroutine);
        foreach (GameObject obj in controllerInfo)
        {
            obj.SetActive(true);
        }
    }
    public void OnControllerExit()
    {
        StopCoroutine(behaviorCorroutine);
        foreach (GameObject obj in controllerInfo)
        {
            obj.SetActive(true);
        }
    }
    IEnumerator RunningBehavior()
    {
        while (true)
        {
            yield return null;
        }
       
        yield break;
    }
}
