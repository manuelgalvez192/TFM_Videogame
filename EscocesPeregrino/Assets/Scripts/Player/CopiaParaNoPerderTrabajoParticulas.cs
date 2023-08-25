using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement256 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float jumpPower;
    [SerializeField] private float floorLevel;
    [NonSerialized] public bool canControl = true;
    private bool isGrounded;
    private float timeToBeGrounded = 0.72f;
    [Header("Particle System")]
    [SerializeField] ParticleSystem dustMovementPS;
    [SerializeField] ParticleSystem dustJumpPS;
    float movementDustCount;
    [SerializeField][Range(0,0.1f)] float movementDustRate = 0.08f;

    private void Start()
    {
        PlayerBasicAtack.canMove += ChangeMoveOption;
        isGrounded = true;
       // floorLevel = transform.localScale.y;
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
        if(transform.localScale.y <= floorLevel|| isGrounded)
        {
            IsInGround();
            animator.SetBool("isJumping", false);
        }
        
    }

    private void Update()
    {
        if (InputsGameManager.instance.JumpButtonDown)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping",true);
            Jump();
        
        }
        if (!isGrounded)
        {
            timeToBeGrounded -= Time.deltaTime;
            if (timeToBeGrounded <= 0)
            {
                isGrounded = true;
                timeToBeGrounded = 0.72f;
                //particulas salto
                ThrowJumpDust();
                movementDustCount = 0;
            }
        }
        //Cosas de particulas
        if (rb.velocity.magnitude > 1 && isGrounded)
        {
            movementDustCount += Time.deltaTime;
            if (movementDustCount >= movementDustRate)
            {
                movementDustCount = 0;
                ThrowMovementDust();
            }
        }
        else
        {
            movementDustCount = 0;
            StopMovementDust();
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            isGrounded= false;
            rb.gravityScale = 0.3f;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            //floorLevel = transform.localScale.y -1;
            ThrowJumpDust();
        }
    }

    private void IsInGround()
    {
        rb.gravityScale = 0;
        isGrounded= true;
        animator.SetBool("isRunning", false);

        if (canControl)
        {
            rb.velocity = new Vector2(speed *InputsGameManager.instance.HorizontalAxis, rb.velocity.y);

            rb.velocity = new Vector2(rb.velocity.x, speed * InputsGameManager.instance.VerticalAxis);

            if (rb.velocity.x > 0)
            {
                animator.SetBool("isRunning", true);
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else
                if (rb.velocity.x < 0)
            {
                animator.SetBool("isRunning", true);
                transform.localScale = new Vector2(-1, transform.localScale.y);
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
  
        // floorLevel = transform.localScale.y;

    }
    void ThrowMovementDust()
    {
        dustMovementPS.Play();
    }
    void StopMovementDust()
    {
        dustMovementPS.Stop();
    }
    void ThrowJumpDust()
    {
        dustJumpPS.Play();
    }
}
