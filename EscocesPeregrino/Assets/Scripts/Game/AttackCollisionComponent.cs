using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class AttackCollisionComponent : MonoBehaviour
{
    [SerializeField] bool isPlayerCollision = false;
    public float damageAmount;
    [SerializeField] UnityEvent otherActions;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("LifeComponent"))
        {
            if(isPlayerCollision)
            {
                other.GetComponent<LifeComponent>().GetDamage(damageAmount);
            }
            else
            {
                other.GetComponent<PlayerLife>().GetDamage(damageAmount);
            }
            otherActions.Invoke();
        }
    }
}