using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAtack : MonoBehaviour
{

    [SerializeField]
    private GameObject hitbox;
    [SerializeField]
    private Animator animator;
    private Vector2 gamepadInput;
    
    private int comboCount;
    private bool atacking;

    public delegate bool CanMove(bool value);
    public static CanMove canMove;

    void Start()
    {
        atacking = false;
        comboCount = 0;
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && !atacking)
        {
            Combo();
        }
    }

    private void OnBasicAtack()//gamepad button pressed
    {
        if (!atacking)
        {
            canMove?.Invoke(false);
            hitbox.SetActive(true);
            atacking = true;
            animator.SetTrigger("" + comboCount);
        }
    }

    public void Combo()
    {
        canMove?.Invoke(false);
        hitbox.SetActive(true);
        atacking = true;
        animator.SetTrigger("" + comboCount);
    }
    
    public void StartCombo()
    {
        atacking = false;
        if (comboCount < 3)
        {
            comboCount++;
        }
    }

    public void FinishCombo()
    {
        canMove?.Invoke(true);
        atacking = false;
        comboCount = 0;
        hitbox.SetActive(false);
    }
}
