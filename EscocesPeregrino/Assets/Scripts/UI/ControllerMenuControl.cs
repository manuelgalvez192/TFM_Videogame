using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    Gamepad playerGamePad;

    PadInput playPadButton;
    PadInput controlsPadButton;
    PadInput exitPadPadButton;
    PadInput backPadButton;
    PadInput prevPadButton;
    PadInput nextPadButton;

    IEnumerator behaviorCorroutine;
    private void Awake()
    {
        behaviorCorroutine = RunningBehavior();
    }

    public void InitiatializeInputs()
    {
        playerGamePad = InputsGameManager.instance.PlayerGamePad;

        playPadButton = new PadInput();
        playPadButton.PadButton = playerGamePad.buttonNorth;

        controlsPadButton = new PadInput();
        controlsPadButton.PadButton = playerGamePad.buttonWest;

        exitPadPadButton = new PadInput();
        exitPadPadButton.PadButton = playerGamePad.buttonEast;

        backPadButton = new PadInput();
        backPadButton.PadButton = playerGamePad.startButton;

        prevPadButton = new PadInput();
        prevPadButton.PadButton = playerGamePad.leftShoulder;

        nextPadButton = new PadInput();
        nextPadButton.PadButton = playerGamePad.rightShoulder;
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
            obj.SetActive(false);
        }
    }
    void ChangeControlsInfo(bool next)
    {
        currentButton += next ? 1 : -1;
        if (currentButton < 0)
            currentButton = 2;
        else if (currentButton > 2)
            currentButton = 0;

        changeControlsButton[currentButton].onClick.Invoke();
    }
    IEnumerator RunningBehavior()
    {
        while (true)
        {
            if (playPadButton.GetPadButtonUp())
                startButton.onClick.Invoke();
            else if (controlsPadButton.GetPadButtonUp())
                controlsButton.onClick.Invoke();
            else if (exitPadPadButton.GetPadButtonUp())
                exitButton.onClick.Invoke();
            else if(controlsPanel.activeInHierarchy)
            {
                if (backPadButton.GetPadButtonUp())
                    backButton.onClick.Invoke();
                else if (prevPadButton.GetPadButtonUp())
                    ChangeControlsInfo(false);
                else if (nextPadButton.GetPadButtonUp())
                    ChangeControlsInfo(true);
            }



            yield return null;
        }
       
        yield break;
    }
}
