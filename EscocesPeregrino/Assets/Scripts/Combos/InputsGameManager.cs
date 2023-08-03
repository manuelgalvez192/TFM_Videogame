using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsGameManager : MonoBehaviour
{
    float verticalAxis;
    float horizontalAxis;

    bool attackInput;
    bool jumpInput;
    bool dashInput;
    bool coverInput;
    bool pickInput;



    Gamepad playerGamePad;


#if UNITY_STANDALONE || UNITY_EDITOR
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode attackKey = KeyCode.J;
    [SerializeField] KeyCode dashKey = KeyCode.K;
    [SerializeField] KeyCode coverKey = KeyCode.L;
    [SerializeField] KeyCode pickKey = KeyCode.L;

#endif
    //padbuttons
    public PadInput attackPadButton;
    public PadInput JumpPadButton;
    public PadInput dashPadButton;
    public PadInput coverPadButton;
    public PadInput pickPadButton;


    public static InputsGameManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);

        playerGamePad = Gamepad.current;
        if(playerGamePad!=null)
        {
            attackPadButton.PadButton = playerGamePad.buttonWest;
            JumpPadButton.PadButton = playerGamePad.buttonSouth;
            dashPadButton.PadButton = playerGamePad.buttonNorth;
            coverPadButton.PadButton = playerGamePad.buttonEast;
            pickPadButton.PadButton = playerGamePad.dpad.right; 

        }
    }
    //Getters

    public float VerticalAxis 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            return Input.GetAxis("Vertical");
#endif

        }
    }
    public float HorizontalAxis 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            return Input.GetAxis("Horizontal");
#endif

        }
    }
    public bool AttackButton
    {
        get
        {
            if(playerGamePad!=null)
                attackInput= attackPadButton.GetPadButton();
#if UNITY_STANDALONE || UNITY_EDITOR
            if(!attackInput)
            attackInput= Input.GetKey(attackKey);
#endif
            return attackInput;

        }
    }
    public bool AttackButtonDown
    {
        get
        {

            if (playerGamePad!=null)
                attackInput = attackPadButton.GetPadButtonDown();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!attackInput)

                attackInput =  Input.GetKeyDown(attackKey);
#endif
            return attackInput;

        }

    }
    public bool AttackButtonUp
    {
        get
        {

            if (playerGamePad != null)
                attackInput = attackPadButton.GetPadButtonUp();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!attackInput)

                attackInput = Input.GetKeyUp(attackKey);
#endif
            return attackInput;

        }
    }
    public bool JumpButton
    {
        get
        {

            if (playerGamePad != null)
                jumpInput = JumpPadButton.GetPadButton();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!jumpInput)

                jumpInput = Input.GetKey(jumpKey);
#endif
            return jumpInput;

        }
    }
    public bool JumpButtonDown
    {
        get
        {

            if (playerGamePad != null)
                jumpInput = JumpPadButton.GetPadButtonDown();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!jumpInput)

                jumpInput = Input.GetKeyDown(jumpKey);
#endif
            return jumpInput;

        }

    }
    public bool JumpButtonUp
    {
        get
        {

            if (playerGamePad != null)
                jumpInput = JumpPadButton.GetPadButtonUp();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!jumpInput)
                jumpInput = Input.GetKeyUp(jumpKey);
#endif
            return jumpInput;
        }
    }
    public bool DashButton
    {
        get
        {

            if (playerGamePad != null)
                dashInput = dashPadButton.GetPadButton();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!dashInput)

                dashInput = Input.GetKey(dashKey);
#endif
            return dashInput;
        }
    }
    public bool DashButtonDown
    {
        get
        {

            if (playerGamePad != null)
                dashInput = dashPadButton.GetPadButtonDown();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!dashInput)

                dashInput = Input.GetKeyDown(dashKey);
#endif
            return dashInput;
        }

    }
    public bool DashButtonUp
    {
        get
        {

            if (playerGamePad != null)
                dashInput = dashPadButton.GetPadButtonUp();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!dashInput)

                dashInput = Input.GetKeyUp(dashKey);
#endif
            return dashInput;

        }
    }
    public bool CoverButton
    {
        get
        {

            if (playerGamePad != null)
                coverInput = coverPadButton.GetPadButton();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!coverInput)

                coverInput = Input.GetKey(coverKey);
#endif
            return coverInput;

        }
    }
    public bool CoverButtonDown
    {
        get
        {

            if (playerGamePad != null)
                coverInput = coverPadButton.GetPadButtonDown();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!coverInput)

                coverInput = Input.GetKeyDown(coverKey);
#endif
            return coverInput;

        }

    }
    public bool CoverButtonUp
    {
        get
        {

            if (playerGamePad != null)
                coverInput = coverPadButton.GetPadButtonUp();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!coverInput)

                coverInput = Input.GetKeyUp(coverKey);
#endif
            return coverInput;

        }
    }
    public bool PickButton
    {
        get
        {

            if (playerGamePad != null)
                pickInput = pickPadButton.GetPadButton();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!pickInput)

                pickInput = Input.GetKey(pickKey);
#endif
            return pickInput;

        }
    }
    public bool PickButtonDown
    {
        get
        {

            if (playerGamePad != null)
                pickInput = pickPadButton.GetPadButtonDown();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!pickInput)

                pickInput = Input.GetKeyDown(pickKey);
#endif
            return pickInput;

        }

    }
    public bool PickButtonUp
    {
        get
        {

            if (playerGamePad != null)
                pickInput = pickPadButton.GetPadButtonUp();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!pickInput)
                pickInput = Input.GetKeyUp(pickKey);

#endif
            return pickInput;
        }
    }
}
[System.Serializable]
public class PadInput
{
    UnityEngine.InputSystem.Controls.ButtonControl padButton;
    bool isPressed = false;
    bool hasBeenPressed = false;
    public UnityEngine.InputSystem.Controls.ButtonControl PadButton { set { padButton = value; }}

    public bool GetPadButtonDown()
    {
        isPressed = padButton.isPressed;
        if (padButton.isPressed && !hasBeenPressed)
        {
            hasBeenPressed = true;
            isPressed = true;
            return true;
        }
        if (!padButton.isPressed)
            hasBeenPressed = false;
        return false;
    }
    public bool GetPadButton()
    {
        isPressed = padButton.isPressed;
        if (!padButton.isPressed)
            hasBeenPressed = false;
        return padButton.isPressed;
    }
    public bool GetPadButtonUp()
    {
        if (padButton.isPressed)
            isPressed = true;
        else if(isPressed)
        {
            isPressed = false;
            return true;
        }
        return false;
    }

}

