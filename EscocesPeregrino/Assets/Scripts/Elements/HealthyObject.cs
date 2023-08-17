using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyObject : PickeableObject
{
    [SerializeField] PlayerLife lifeComponent;
    [SerializeField] float healAmount;
    void OnEnable()
    {
        lifeComponent = PlayerSingleton.instance.playerLife;
        Select();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerLife>()!=null && other.GetComponent<PlayerLife>() == lifeComponent)
        {
            lifeComponent.GetHeal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
