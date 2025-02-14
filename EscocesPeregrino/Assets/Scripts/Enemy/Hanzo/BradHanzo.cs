using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BradHanzo : BaseHanzo
{
    [SerializeField]Collider2D attackCollider;
    //[SerializeField] GameObject collision;
    void Start()
    {
        base.Start();
    }
    protected override void ChangeState(HanzoState newState)
    {
        base.ChangeState(newState);
        switch (newState)
        {
            case HanzoState.Waiting:
                currentCorroutine = OnWaitingState();
                break;
            case HanzoState.Following:
                currentCorroutine = OnFollowPlayerState();
                break;
            case HanzoState.CheckingLastPositon:
                currentCorroutine = OnCheckingLasPlayerPosState();
                break;
            case HanzoState.Attacking:
                currentCorroutine = OnAttackingState();
                break;
            case HanzoState.GettingDamage:
                currentCorroutine = OnDamageState();
                break;
            default:
                break;
        }
        StartCoroutine(currentCorroutine);
    }
    IEnumerator OnWaitingState()
    {
        animator.SetBool("isMoving", false);
        while (DistanceToPlayer() > detectionRange)
        {
            if (DistanceToPlayer() < attackDistance)
                ChangeState(HanzoState.Attacking);
            yield return null;
        }
        int randAction = Random.Range(1, 101);
        ChangeState(HanzoState.Following);
        yield break;
    }
    IEnumerator OnFollowPlayerState()
    {
        animator.SetBool("isMoving", true);
        while (DistanceToPlayer() < detectionRange)
        {
            FollowPlayer();
            if (DistanceToPlayer() < attackDistance)
                ChangeState(HanzoState.Attacking);
            yield return null;
        }
        ChangeState(HanzoState.CheckingLastPositon);
        yield break;
    }
    IEnumerator OnCheckingLasPlayerPosState()
    {
        animator.SetBool("isMoving", true);
        Vector2 lastPlayerPosition = player.position;
        while (Vector2.Distance(lastPlayerPosition, transform.position) > 0.01f)
        {
            MoveToPoint(lastPlayerPosition);
            if (DistanceToPlayer() < detectionRange)
                ChangeState(HanzoState.Following);
            yield return null;
        }
        ChangeState(HanzoState.Waiting);
        yield break;
    }
    IEnumerator OnAttackingState()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("attack");
        AudioManager.instance.PlayOneShot(FMODEvents.instance.enemieSword, this.transform.position);
        yield return new WaitForSeconds(0.25f);//los tiempos se cambian segun la animacion
        attackCollision.size = new Vector2(0, attackCollision.size.y);
        attackCollision.enabled = true;
        float newSize = 0;
        while (newSize <= attackCollisionSize)
        {
            newSize += 0.05f;
            attackCollision.size = new Vector2(newSize, attackCollision.size.y);
        }
        attackCollision.size = new Vector2(attackCollisionSize, attackCollision.size.y);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.enemiePunch, this.transform.position);


        yield return new WaitForSeconds(0.13f);
        attackCollision.enabled = false;

        yield return new WaitForSeconds(attackRate);

        ChangeState(DistanceToPlayer() < attackDistance ? HanzoState.Attacking : HanzoState.CheckingLastPositon);
        yield break;
    }
    IEnumerator OnDamageState()
    {
        animator.SetTrigger("damage");
        yield return new WaitForSeconds(0.9f);
        ChangeState(HanzoState.Waiting);

        yield break;
    }
    public override void GetDamage()
    {
        if (!allowedToMove)
            return;
        StopCoroutine(currentCorroutine);
        base.GetDamage();
        ChangeState(HanzoState.GettingDamage);
    }
    public override void Die()
    {
        base.Die();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.enemieDeath, this.transform.position);
        StopCoroutine(currentCorroutine);
        animator.SetTrigger("die");
    }
}
