using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    private float actualHealth;
    
    void Start()
    {
        actualHealth = maxHealth;
    }

    private void ReduceLife(float damageAmount)
    {
        actualHealth -= damageAmount;
    }

    private void OnCollisionEnter(Collision other)
    {
        //if(other.gameObject.tag())
    }
}
