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
    void Start()
    {
        
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
        if (InputsGameManager.instance.HorizontalAxis != 0 || InputsGameManager.instance.VerticalAxis != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if(InputsGameManager.instance.AttackButtonDown)
        {
            anim.SetTrigger("Attack");
        }
        if (InputsGameManager.instance.JumpButton)
        {
            anim.SetTrigger("Jump");
        }
        if (InputsGameManager.instance.CoverButtonUp)
        {
            print("Cover");
        }
    }
    void Movement()
    {
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


}
