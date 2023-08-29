using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col2D;

    [SerializeField] private Transform render;
    [SerializeField,Range(0.01f,0.05f)]private float jumpPower =0.02f;
    [SerializeField] private float floorLevel;
    /*[NonSerialized]*/ public bool canControl = true;

    public bool isGrounded;
    private float timeToBeGrounded = 0.72f;

    public bool isBlocking;

    [Header("Particle System")]
    [SerializeField] ParticleSystem dustMovementPS;
    [SerializeField] ParticleSystem dustJumpPS;
    float movementDustCount;
    [SerializeField,Range(0, 0.1f)] float movementDustRate = 0.08f;

    private void Start()
    {
        PlayerBasicAtack.canMove += ChangeMoveOption;
        isGrounded = true;
        isBlocking = false;
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

    private void Update()
    {
        if (!canControl)
            return;
        if (InputsGameManager.instance.JumpButtonDown)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping",true);
            Jump();
        
        }
        if (transform.localScale.y <= floorLevel || isGrounded)
        {
            IsInGround();
            animator.SetBool("isJumping", false);
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
        if(!isBlocking)
        {
            if (isGrounded)
            {
                isGrounded = false;

                StartCoroutine(JumpBehaviour());
                ThrowJumpDust();
            }
        }
    }

    IEnumerator JumpBehaviour()
    {
        float currenthigh=0;
        col2D.enabled= false;
        while (currenthigh<=0.4)
        {
            
            currenthigh += Time.deltaTime;
            render.localPosition=new Vector2 (render.localPosition.x, render.localPosition.y + currenthigh * jumpPower);
            yield return null;
        }
        while(currenthigh>0)
        {
            
            currenthigh -=Time.deltaTime;
            render.localPosition = new Vector2(render.localPosition.x, render.localPosition.y - currenthigh * jumpPower);
            yield return null;
            
        }
        isGrounded = true;
        col2D.enabled = true; 
        render.localPosition = Vector2.zero;
        animator.SetBool("isJumping", false);
        ThrowJumpDust();
        movementDustCount = 0;
        yield break;
    }

    private void IsInGround()
    {
        rb.gravityScale = 0;
        isGrounded= true;
        animator.SetBool("isRunning", false);

        if(!isBlocking)
        {
            if (canControl)
            {
                rb.velocity = new Vector2(speed * InputsGameManager.instance.HorizontalAxis, rb.velocity.y);

                rb.velocity = new Vector2(rb.velocity.x, speed * InputsGameManager.instance.VerticalAxis);

                if (rb.velocity.x > 0)
                {
                    animator.SetBool("isRunning", true);
                    render.localScale = new Vector2(1, render.localScale.y);
                }
                else
                    if (rb.velocity.x < 0)
                {
                    animator.SetBool("isRunning", true);
                    render.localScale = new Vector2(-1, render.localScale.y);
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
        else if(isBlocking)
        {

        }



    }
    public void StopMovement()
    {
        canControl = false;
        rb.velocity = Vector2.zero;
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
