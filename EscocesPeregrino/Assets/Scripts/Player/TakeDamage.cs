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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReduceLife(float damageAmount)
    {
        actualHealth -= damageAmount;
    }
}
