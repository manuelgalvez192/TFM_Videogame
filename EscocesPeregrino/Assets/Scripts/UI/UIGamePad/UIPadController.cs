using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject joyStick;



    //Funcionality
    bool isJoystickActive = false;

    float horizontalAxis = 0;
    float verticalAxis =0 ;

    [SerializeField] UIPadButton jumpButton;
    [SerializeField] UIPadButton attackButton;
    [SerializeField] UIPadButton coverButton;
    [SerializeField] UIPadButton dashButton;
    [SerializeField] bool isMenuControls = false;
    private void OnEnable()
    {
        if (!isMenuControls&&!InputsGameManager.instance.virtualPadEnabled  )
            gameObject.SetActive(false);
        InputsGameManager.instance.UIController = this;
        jumpButton.ControllerParent = this;
        attackButton.ControllerParent = this;
        coverButton.ControllerParent = this;
        dashButton.ControllerParent = this;
    }

    //gettersSetters
    public bool IsJoystickActive
    {
        get { return isJoystickActive; }
    }

    #region Joystick
    public float HorizontalAxis
    {
        get { return horizontalAxis; }
        set { horizontalAxis = value; }
    }
    public float VerticalAxis
    {
        get { return verticalAxis; }
        set { verticalAxis = value; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickActive = true;

        //jpystick things
        joyStick.SetActive(true);
        joyStick.transform.position = Input.mousePosition;
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickActive = false;
        joyStick.SetActive(false);
    }
    void ResetValues()//Aqui reseteamops todos los valores
    {
        horizontalAxis = 0;
        verticalAxis = 0;
    }
    #endregion
    #region Buttons
    //down
    public bool JumpButtonDown
    {
        get { return jumpButton.GetPadButtonDown(); }
    }
    public bool CoverButtonDown
    {
        get { return coverButton.GetPadButtonDown(); }
    }
    public bool DashButtonDown
    {
        get { return dashButton.GetPadButtonDown(); }
    }
    public bool AttackButtonDown
    {
        get { return attackButton.GetPadButtonDown(); }
    }
    //pressed
    public bool JumpButton
    {
        get { return jumpButton.GetPadButton(); }
    }
    public bool CoverButton
    {
        get { return coverButton.GetPadButton(); }
    }
    public bool DashButton
    {
        get { return dashButton.GetPadButton(); }
    }
    public bool AttackButton
    {
        get { return attackButton.GetPadButton(); }
    }
    //Up
    public bool JumpButtonUp
    {
        get { return jumpButton.GetPadButtonUp(); }
    }
    public bool CoverButtonUp
    {
        get { return coverButton.GetPadButtonUp(); }
    }
    public bool DashButtonUp
    {
        get { return dashButton.GetPadButtonUp(); }
    }
    public bool AttackButtonUp
    {
        get { return attackButton.GetPadButtonUp(); }
    }
    #endregion

}

