using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAtack : MonoBehaviour
{

    [SerializeField] private GameObject hitbox;
    [SerializeField] private Animator animator;
    private Gamepad gamepad;
    
    private int comboCount;
    private bool atacking;

    public delegate bool CanMove(bool value);
    public static CanMove canMove;

    void Start()
    {
        gamepad = Gamepad.current;
        atacking = false;
        comboCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || gamepad.buttonWest.isPressed)
        {
            Combo();
        }
    }

    public void Combo()
    {
        if (!atacking)
        {
            canMove?.Invoke(false);
            hitbox.SetActive(true);
            atacking = true;
            animator.SetTrigger("" + comboCount);
        }
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
        comboCount = 0;
        canMove?.Invoke(true);
        atacking = false;
        hitbox.SetActive(false);
    }
}
