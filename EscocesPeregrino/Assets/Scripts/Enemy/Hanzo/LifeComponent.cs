using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class LifeComponent : MonoBehaviour
{
    [SerializeField] float maxLife = 6;
    public float currentLife;

    [SerializeField] string damagedTag;
    [SerializeField] UnityEvent DieAction;
    [SerializeField] UnityEvent DamageAction;

    void OnEnable()
    {
        currentLife = maxLife;
    }

    public void GetDamage(float Damage)
    {
        currentLife -= Damage;
        if(currentLife<=0)
        {
            DieAction.Invoke();
        }
        else
            DamageAction.Invoke();
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag ==damagedTag)
    //    {
    //        GetDamage(PlayerSingleton.instance.playerAttackDamage);
    //    }
    //}
}
