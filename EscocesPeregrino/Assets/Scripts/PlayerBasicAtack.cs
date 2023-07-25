using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAtack : MonoBehaviour
{

    private Animator animator;
    private Vector2 gamepadInput;
    
    private int comboCount;
    private bool atacking;
    
    void Start()
    {
        animator = GetComponent<Animator>();
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
            atacking = true;
            animator.SetTrigger(""+comboCount);
        }
    }

    public void Combo()
    {
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
        atacking = false;
        comboCount = 0;
    }
}
