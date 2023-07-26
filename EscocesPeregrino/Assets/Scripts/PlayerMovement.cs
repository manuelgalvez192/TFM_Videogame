using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Rigidbody rb;
    
    private Vector2 gamepadInput;

    private bool canControl = true;

    private void OnMovement(InputValue value)
    {
        gamepadInput = value.Get<Vector2>();
    }

    private void Start()
    {
        PlayerBasicAtack.canMove += ChangeMoveOption;
    }

    private bool ChangeMoveOption(bool value)
    {
        canControl = value;
        return canControl;
    }

    private void OnDisable()
    {
        PlayerBasicAtack.canMove -= ChangeMoveOption;
    }

    private void FixedUpdate()
    {
        animator.SetBool("isRunning", false);

        if (canControl)
        {
            if (Input.GetKey(KeyCode.A) || gamepadInput.x < 0)
            {
                rb.velocity = new Vector2(speed * -1, rb.velocity.y);
                animator.SetBool("isRunning", true);
                sr.flipX = true;
            }

            if (Input.GetKey(KeyCode.D) || gamepadInput.x > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                animator.SetBool("isRunning", true);
                sr.flipX = false;
            }
        }
    }
}
