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
    private bool pauseInput;
    private bool moveMenuUpInput;
    private bool moveMenuDownInput;



    Gamepad playerGamePad;

    [SerializeField]UIPadController uiController;
    public UIPadController UIController { set { uiController = value; } }


#if UNITY_STANDALONE || UNITY_EDITOR
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode attackKey = KeyCode.J;
    [SerializeField] KeyCode dashKey = KeyCode.K;
    [SerializeField] KeyCode coverKey = KeyCode.L;
    [SerializeField] KeyCode pickKey = KeyCode.E;
    [SerializeField] KeyCode pauseKey = KeyCode.P;
    [SerializeField] KeyCode moveMenuUpKey = KeyCode.W;
    [SerializeField] KeyCode moveMenuDownKey = KeyCode.S;

#endif
    //padbuttons
     PadInput attackPadButton;
     PadInput JumpPadButton;
     PadInput dashPadButton;
     PadInput coverPadButton;
     PadInput pickPadButton;
     PadInput pausePadButton;
     PadInput moveMenuUpPadButton;
     PadInput moveMenuDownPadButton;



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
            attackPadButton = new PadInput();
            JumpPadButton = new PadInput();
            dashPadButton = new PadInput();
            coverPadButton = new PadInput();
            pickPadButton = new PadInput();
            pausePadButton = new PadInput();
            moveMenuUpPadButton    =new PadInput();
            moveMenuDownPadButton = new PadInput();

            attackPadButton.PadButton = playerGamePad.buttonWest;
            JumpPadButton.PadButton = playerGamePad.buttonSouth;
            dashPadButton.PadButton = playerGamePad.buttonNorth;
            coverPadButton.PadButton = playerGamePad.buttonEast;
            pickPadButton.PadButton = playerGamePad.dpad.right;
            pausePadButton.PadButton = playerGamePad.startButton;
            moveMenuUpPadButton.PadButton = playerGamePad.dpad.up;
            moveMenuDownPadButton.PadButton = playerGamePad.dpad.down;
        }
    }

    //Getters

    public float VerticalAxis 
    { get 
        {
            if(uiController!=null && uiController.IsJoystickActive)
            {
                verticalAxis = uiController.VerticalAxis;
            }
            else
            verticalAxis = Input.GetAxis("Vertical");
            return verticalAxis;
        }
    }
    public float HorizontalAxis 
    { get 
        {
            if (uiController != null && uiController.IsJoystickActive)
            {
                horizontalAxis = uiController.HorizontalAxis;
            }
            else
                horizontalAxis = Input.GetAxis("Horizontal");
            return horizontalAxis;
        }
    }
    public bool AttackButton
    {
        get
        {
            if (uiController != null)
            {
                attackInput = uiController.AttackButton;
                if(attackInput)
                return attackInput;
            }

            attackInput = false;
            
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
            if (uiController != null)
            {
                attackInput = uiController.AttackButtonDown;
                if (attackInput)
                return attackInput;
            }
            attackInput = false;

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
            if (uiController != null)
            {
                attackInput = uiController.AttackButtonUp;
                if (attackInput)
                    return attackInput;
            }
            attackInput = false;

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
            if (uiController != null)
            {
                jumpInput = uiController.JumpButton;
                if(jumpInput)
                return jumpInput;
            }
            jumpInput = false;

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
            if (uiController != null)
            {
                jumpInput = uiController.JumpButtonDown;
                if (jumpInput)
                    return jumpInput;
            }
            jumpInput = false;

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
            if (uiController != null)
            {
                jumpInput = uiController.JumpButtonUp;
                if (jumpInput)
                    return jumpInput;
            }
            jumpInput = false;

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
            if (uiController != null)
            {
                dashInput = uiController.DashButton;
                if (dashInput)
                    return dashInput;
            }
            dashInput = false;

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
            if (uiController != null)
            {
                dashInput = uiController.DashButtonDown;
                if (dashInput)
                    return dashInput;
            }
            dashInput = false;

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
            if (uiController != null)
            {
                dashInput = uiController.DashButtonUp;
                if (dashInput)
                    return dashInput;
            }
            dashInput = false;

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
            if (uiController != null)
            {
                coverInput = uiController.CoverButton;
                if(coverInput)
                return coverInput;
            }
            coverInput = false;

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
            if (uiController != null)
            {
                coverInput = uiController.CoverButtonDown;
                if (coverInput)
                    return coverInput; return coverInput;
            }
            coverInput = false;

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
            if (uiController != null)
            {
                coverInput = uiController.CoverButtonUp;
                if (coverInput)
                    return coverInput; return coverInput;
            }
            coverInput = false;

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
            pickInput = false;

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
            pickInput = false;
            
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
            pickInput = false;

            if (playerGamePad != null)
                pickInput = pickPadButton.GetPadButtonUp();
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!pickInput)
                pickInput = Input.GetKeyUp(pickKey);

#endif
            return pickInput;
        }
    }
    
    //pause buttons
    public bool PauseButtonDown
    {
        get
        {
            pauseInput = false;
            
            if (playerGamePad != null)
                pauseInput = pausePadButton.GetPadButtonDown();
            
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!pauseInput)
                pauseInput = Input.GetKeyDown(pauseKey);
            
#endif
            return pauseInput;
        }
    }
    
    public bool MoveMenuUpDown
    {
        get
        {
            moveMenuUpInput = false;
            
            if (playerGamePad != null)
                moveMenuUpInput = moveMenuUpPadButton.GetPadButtonDown();
            
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!moveMenuUpInput)
                moveMenuUpInput = Input.GetKeyDown(moveMenuUpKey);
            
#endif
            return moveMenuUpInput;
        }
    }
    
    public bool MoveMenuDownDown
    {
        get
        {
            moveMenuDownInput = false;
            
            if (playerGamePad != null)
                moveMenuDownInput = moveMenuDownPadButton.GetPadButtonDown();
            
#if UNITY_STANDALONE || UNITY_EDITOR
            if (!moveMenuDownInput)
                moveMenuDownInput = Input.GetKeyDown(moveMenuDownKey);
            
#endif
            return moveMenuDownInput;
        }
    }


    public void ResetInputs()
    {
        horizontalAxis = 0;
        verticalAxis = 0;
        attackInput=false;
        jumpInput  =false;
        dashInput  =false;
        coverInput =false;
        pickInput = false;
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

