using System.Collections;
using System.Collections.Generic;
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

    private int dir = 1;

    private bool canControl = true;

    private void OnMovement(InputValue value)
    {
        gamepadInput = value.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        animator.SetBool("isRunning", false);
        
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
