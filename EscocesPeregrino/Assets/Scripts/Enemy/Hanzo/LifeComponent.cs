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
    [SerializeField] Transform particlePos;

    [SerializeField] UnityEvent DieAction;
    [SerializeField] UnityEvent DamageAction;
    bool isAlive = true;
    void OnEnable()
    {
        currentLife = maxLife;
    }

    public void GetDamage(float Damage)
    {
        if (!isAlive)
            return;
        currentLife -= Damage;
        ThrowDamageParticle();
        if (currentLife <= 0)
        {
            DieAction.Invoke();
            isAlive = false;
        }
        else
            DamageAction.Invoke();
    }
    void ThrowDamageParticle()
    {
        int rand = Random.Range(0, 2);
        if (PlayerSingleton.instance.isSpecialAttack)
        {
            ParticleSystemManager.instance.ThrowParticleSystem(rand == 0 ? "Boom" : "Fire", particlePos);
        }
        else
            ParticleSystemManager.instance.ThrowParticleSystem(rand == 0 ? "BasicHit" : "RandomText", particlePos);

        ParticleSystemManager.instance.ThrowParticleSystem("Blood", particlePos);
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag ==damagedTag)
    //    {
    //        GetDamage(PlayerSingleton.instance.playerAttackDamage);
    //    }
    //}
}
