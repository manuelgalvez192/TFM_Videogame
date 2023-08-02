using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsGameManager : MonoBehaviour
{
    float verticalAxis;
    float horizontalAxis;

    bool jumpInput;
    bool attackInput;
    bool dashInput;
    bool coverInput;

    public static InputsGameManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

#if UNITY_STANDALONE||UNITY_EDITOR
    private void Update()
    {

        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        attackInput = Input.GetKeyDown(KeyCode.J);
        dashInput = Input.GetKeyDown(KeyCode.K);
        coverInput = Input.GetKeyDown(KeyCode.L);
    }
#endif

    //Getters
    bool JumpInput { get { return jumpInput; } }
    bool AttackInput { get { return attackInput; } }
    bool DashInput { get { return dashInput; } }
    bool CoverInput { get { return coverInput; } }

    float VerticalAxis { get { return verticalAxis; } }
    float HorizontalAxis { get { return horizontalAxis; } }
}

