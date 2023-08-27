using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoControl : BaseHanzo
{
    public float TPOffset = 0.5f;
    void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            ChangeState(HanzoState.SpecialAction);
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
            case HanzoState.SpecialAction:
                currentCorroutine = OnSpecialAttackState();
                break;
            default:
                break;
        }
        //state = newState;
        StartCoroutine(currentCorroutine);
    }

    IEnumerator OnWaitingState()
    {
        animator.SetBool("isMoving", false);
        while (DistanceToPlayer()>detectionRange)
        {
            if (DistanceToPlayer() < attackDistance)
                ChangeState(HanzoState.Attacking);
            yield return null;
        }
        ChangeState(HanzoState.Following);
        yield break;
    }
    IEnumerator OnFollowPlayerState()
    {
        animator.SetBool("isMoving", true);
        while (DistanceToPlayer()<detectionRange)
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
        while (Vector2.Distance(lastPlayerPosition,transform.position)>0.01f)
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
        yield return new WaitForSeconds(0.15f);//los tiempos se cambian segun la animacion
        attackCollision.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        attackCollision.SetActive(false);

        yield return new WaitForSeconds(attackRate);

        ChangeState(DistanceToPlayer() < attackDistance ? HanzoState.Attacking : HanzoState.Waiting);
        yield break;
    }
    IEnumerator OnSpecialAttackState()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("specialAttack");

        yield return new WaitForSeconds(2.4f);
        float rot;
        if (PlayerSingleton.instance.gameObject.transform.GetChild(0).transform.localScale.x < 0)
            rot = 1;
        else
            rot = -1;
        transform.position = new Vector2(PlayerSingleton.instance.gameObject.transform.position.x + TPOffset*rot ,PlayerSingleton.instance.gameObject.transform.position.y);

        Vector2 playerDir = player.position - transform.position;

        //playerDir.Normalize();

        if (playerDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        yield return new WaitForSeconds(2.4f);
        ChangeState(HanzoState.Attacking);
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
        StopCoroutine(currentCorroutine);
        base.GetDamage();
        ChangeState(HanzoState.GettingDamage);
    }
    public override void Die()
    {
        base.Die();
        StopCoroutine(currentCorroutine);
        animator.SetTrigger("die");
    }

}
