using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;

    private bool canControl = true;

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
            rb.velocity = new Vector2(speed*Input.GetAxis("Horizontal"),rb.velocity.y);
            
            rb.velocity = new Vector2(rb.velocity.x, speed*Input.GetAxis("Vertical"));

            if (rb.velocity.x > 0)
            {
                animator.SetBool("isRunning", true);
                sr.flipX = false;
            }else
                if (rb.velocity.x < 0)
                {
                    animator.SetBool("isRunning", true);
                    sr.flipX = true;
                }
            
            if (rb.velocity.y > 0 || rb.velocity.y < 0)
            {
                animator.SetBool("isRunning", true);
            }
            
            
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
