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
    [SerializeField,Range(0.1f,1)]private float jumpPower =0.35f;
    [SerializeField]AnimationCurve jumpCurve;
    [SerializeField] private float floorLevel;
    /*[NonSerialized]*/ public bool canControl = true;
    [SerializeField] Transform shadowRender;
    [SerializeField] float shadowMultiplyJump;
    [SerializeField] float shadowMultiplyRunning;
    float currentShadowSinusState;
    Vector3 shadowStartScale;

    public bool isGrounded;
    private float timeToBeGrounded = 0.72f;

    public bool isBlocking;
    private bool isRunning;
    private bool playingStepsSounds;

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
        playingStepsSounds=false;
        // floorLevel = transform.localScale.y;
        shadowStartScale = shadowRender.localScale;
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
        if (canControl)
        {
            if (InputsGameManager.instance.JumpButtonDown)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", true);

                Jump();
            }

            if (isGrounded)
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
        else
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("isRunning", false);
        }

        isRunning = animator.GetBool("isRunning");
        
    }

    private void Jump()
    {
        if(!isBlocking)
        {
            if (isGrounded)
            {
                isGrounded = false;

                StartCoroutine(JumpBehaviour());
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump,this.transform.position);
            }
        }
    }

    IEnumerator JumpBehaviour()
    {
        /*float currenthigh=0;
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
        yield break;*/

        col2D.enabled = false;
        isGrounded = false;
        float currentHeight = 0;
        float maxTime = jumpCurve[jumpCurve.length - 1].time;
        float elapsedTime = 0;
        ThrowJumpDust();
        


        while (elapsedTime <= maxTime)
        {
            currentHeight = jumpCurve.Evaluate(elapsedTime);
            Vector3 newScale = shadowStartScale + shadowStartScale * currentHeight * shadowMultiplyJump;
            shadowRender.localScale = newScale;
            currentHeight*= jumpPower;
            render.localPosition = new Vector2(render.localPosition.x, currentHeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerLand,this.transform.position);
        isGrounded = true;
        col2D.enabled = true;
        render.localPosition = Vector2.zero;
        ThrowJumpDust();

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

                if(rb.velocity != Vector2.zero)
                {
                    currentShadowSinusState += Time.deltaTime * 7.5f;
                    if (currentShadowSinusState >= 2 * 3.14)
                        currentShadowSinusState = 0;
                    float shadowScaley = Mathf.Sin(currentShadowSinusState);
                    shadowRender.localScale = shadowStartScale + shadowStartScale * shadowMultiplyRunning * shadowScaley;
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
            rb.velocity = new Vector2(0, 0);
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
    private IEnumerator WaitTimeToFootStepSound()
    {
        while(isRunning)
        {
            if (!playingStepsSounds)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFootsteps, this.transform.position);
                playingStepsSounds = true;
            }

        }
        if (!isRunning)
        {
            playingStepsSounds = false;
        }
        yield return null;
       
    }
}
