using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsGameManager : MonoBehaviour
{
    float verticalAxis;
    float horizontalAxis;

    bool jumpInput;
    bool attackInput;
    bool dashInput;
    bool coverInput;
    bool pickInput;



    Gamepad playerGamePad;
    public PadInput attackPadButton;

    public static InputsGameManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        playerGamePad = Gamepad.current;
        attackPadButton.PadButton = playerGamePad.buttonWest;
    }
    private void Update()
    {
        if(attackPadButton.GetPadButtonUp())
        {
            print("ahora");
        }
    }


#if UNITY_STANDALONE || UNITY_EDITOR
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode attackKey = KeyCode.J;
    [SerializeField] KeyCode dashKey = KeyCode.K;
    [SerializeField] KeyCode coverKey = KeyCode.L;
    [SerializeField] KeyCode pickKey = KeyCode.L;

#endif








    //Getters
    public bool JumpInput 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            return Input.GetKeyDown(jumpKey);
#endif

        }
    }
    public bool AttackInput 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            if(playerGamePad!=null)
            {
                return playerGamePad.buttonWest.isPressed;
            }
            return Input.GetKeyDown(attackKey);
#endif

        }
    }
    public bool DashInput 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            return Input.GetKeyDown(dashKey);
#endif

        }
    }
    public bool CoverInput 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            return Input.GetKeyDown(coverKey); ;
#endif

        } 
    }
    public bool PickInput 
    { get 
        {
#if UNITY_STANDALONE || UNITY_EDITOR

            return Input.GetKeyDown(pickKey);
#endif

        }
    }
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

