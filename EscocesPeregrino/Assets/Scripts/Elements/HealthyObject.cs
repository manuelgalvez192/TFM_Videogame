using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyObject : PickeableObject
{
    [SerializeField] PlayerLife lifeComponent;
    [SerializeField] float healAmount;
    void Start()
    {
        lifeComponent = PlayerSingleton.instance.playerLife;
        Select();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerLife>()!=null)
        {
            lifeComponent.GetHeal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
