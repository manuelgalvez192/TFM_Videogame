using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerMovement : MonoBehaviour
{
    [SerializeField] float limitXPos;
    [SerializeField] float limitXNeg;
    [SerializeField] float LimitYPos;
    [SerializeField] float limitYNeg;
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] float speedZ;
    [SerializeField] Animator anim;
    [SerializeField] Transform render;

    [Header("Jump")]
    [SerializeField, Range(0.1f, 1)] private float jumpPower = 0.35f;
    [SerializeField] AnimationCurve jumpCurve;
    bool isGrounded =true;
    [Header("Block")]
    bool isBlocking;

    [Header("Particle System")]
    [SerializeField] ParticleSystem dustMovementPS;
    [SerializeField] ParticleSystem dustJumpPS;
    float movementDustCount;
    [SerializeField, Range(0, 0.1f)] float movementDustRate = 0.08f;

    void Start()
    {
        anim.SetBool("isGrounded", true);
    }

    void Update()
    {
        Movement();
        CheckLimits();
        SetAnimatorValues();

    }


    void CheckLimits()
    {
        Mathf.Clamp(transform.localPosition.x, limitXNeg, limitXPos);
        if(transform.localPosition.x>limitXPos)
        {
           transform.localPosition = new Vector3(limitXPos, transform.localPosition.y);
        }
        else if(transform.localPosition.x < limitXNeg)
        {
            transform.localPosition = new Vector3(limitXNeg, transform.localPosition.y);
        }
        if (transform.localPosition.y > LimitYPos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, LimitYPos);
        } 
        else if (transform.localPosition.y < limitYNeg)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,limitYNeg);
        }
    }
    void SetAnimatorValues()
    {
        if (InputsGameManager.instance.CoverButtonDown)
        {
            isBlocking = true;
            anim.SetBool("Cover", true);
        }
            if (InputsGameManager.instance.CoverButtonUp)
        {
            isBlocking = false;
            anim.SetBool("Cover", false);
        }
        if (isBlocking)
            return;
        if (InputsGameManager.instance.HorizontalAxis != 0 || InputsGameManager.instance.VerticalAxis != 0)
        {
            anim.SetBool("isRunning", true);
            ThrowMovementDust();
        }
        else
        {
            anim.SetBool("isRunning", false);
            StopMovementDust();
        }

        if(InputsGameManager.instance.AttackButtonDown)
        {
            anim.SetTrigger("Attack");
            StartCoroutine(AfterAttack(0.3f));
        }
        if (InputsGameManager.instance.JumpButtonDown)
        {
            Jump();
        }
        if(InputsGameManager.instance.DashButtonDown)
        {
            anim.SetTrigger("SpecialAttack");
            StartCoroutine(AfterAttack(0.7f));
            
        }
    }

    private void Jump()
    {
        if (!isBlocking)
        {
            if (isGrounded)
            {
                isGrounded = false;

                StartCoroutine(JumpBehaviour());
            }
        }
    }

    IEnumerator JumpBehaviour()
    {
        isGrounded = false;
        float currentHeight = 0;
        float maxTime = jumpCurve[jumpCurve.length - 1].time;
        float elapsedTime = 0;
        anim.SetBool("isGrounded", false);
        ThrowJumpDust();


        while (elapsedTime <= maxTime)
        {
            currentHeight = jumpCurve.Evaluate(elapsedTime) * jumpPower;
            render.localPosition = new Vector2(render.localPosition.x, currentHeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isGrounded = true;
        anim.SetBool("isGrounded", true);
        render.localPosition = Vector2.zero;
        ThrowJumpDust();

        yield break;
    }
    void Movement()
    {
        if (isBlocking)
            return;
        transform.Translate(transform.right * InputsGameManager.instance.HorizontalAxis * speedX * Time.deltaTime);
        transform.Translate(transform.up * InputsGameManager.instance.VerticalAxis * speedY * Time.deltaTime);

        if (InputsGameManager.instance.HorizontalAxis > 0)
        {
            Quaternion newRot = Quaternion.Euler(new Vector3(transform.rotation.x, 0, 0));
            transform.rotation = newRot;
        }
        else if (InputsGameManager.instance.HorizontalAxis < 0)
        {
            Quaternion newRot = Quaternion.Euler(new Vector3(transform.rotation.x, 180, 0));
            transform.rotation = newRot;
        }
    }
    IEnumerator AfterAttack(float time)
    {
        isBlocking = true;
        yield return new WaitForSeconds(time);
        isBlocking = false;
        yield break;
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
