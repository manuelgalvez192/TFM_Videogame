using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanzoControl : BaseHanzo
{
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
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
                currentCorroutine = CheckingLasPlayerPosState();
                break;
            case HanzoState.Attacking:
                currentCorroutine = AttackingState();
                break;
            case HanzoState.SpecialAction:
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
    IEnumerator CheckingLasPlayerPosState()
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
    IEnumerator AttackingState()
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

}
