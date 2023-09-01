using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoxxaneControl : BaseHanzo
{
    [SerializeField] float longAttackDistance;
    IEnumerator currentFollowingCorroutine;
    bool isPlayerClose = false;
    [SerializeField] BoxCollider2D longAttackCollider;
    [SerializeField] float specialAttackSpeed;
    void Start()
    {
        base.Start();
    }

    protected override void ChangeState(HanzoState newState)
    {
        base.ChangeState(newState);
        if (currentFollowingCorroutine != null)
            StopCoroutine(currentFollowingCorroutine);
        switch (state)
        {
            case HanzoState.Waiting:
                currentCorroutine = OnWaitingState();
                break;
            case HanzoState.Following:
                currentCorroutine = OnFollowingState();
                break;
            case HanzoState.CheckingLastPositon:
                break;
            case HanzoState.GettingDamage:
                break;
            case HanzoState.Attacking:
                break;
            case HanzoState.SpecialAction://tripleAttack
                currentCorroutine = OnTripleAttackState();
                break;
            case HanzoState.SpecialAction2://longAttack
                currentCorroutine = OnLongAttackState();
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
            yield return null;
        }
        ChangeState(HanzoState.Following);
    }

    #region Following
    IEnumerator OnFollowingState()
    {
        animator.SetBool("isMoving", true);
        if (DistanceToPlayer() > detectionRange)
            ChangeState(HanzoState.CheckingLastPositon);

        else if (DistanceToPlayer() > longAttackDistance - (longAttackDistance / 3))
            currentFollowingCorroutine = KeepingDistance();
        else
            currentFollowingCorroutine = FollowingPlayer();

        StartCoroutine(currentFollowingCorroutine);
        
        yield break;
    }
    IEnumerator KeepingDistance()
    {
        isPlayerClose = false;
        float timeAttack = attackRate;
        while(DistanceToPlayer() > longAttackDistance - (longAttackDistance/3))
        {
            Vector2 dir = player.position - transform.position;

            Vector2 newPos = new Vector2( dir.x < 0 ? longAttackDistance : -longAttackDistance,0) + (Vector2)player.position;
            float dist = Vector2.Distance(newPos, transform.position);
            float distX = Mathf.Abs(newPos.x - transform.position.x);
            if (dist>0.1f)
            {
                animator.SetBool("isMoving",true);
                MoveToPoint(newPos,true);

            }
            else
            {
                if (Mathf.Abs(newPos.y - transform.position.y) < 0.01f)
                    animator.SetBool("isMoving", false);

                if (dir.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if( distX < 0.11f)
            {
                if(timeAttack < 0)
                {
                    timeAttack = attackRate;
                    if(Mathf.Abs(newPos.y - transform.position.y) < 0.01f)
                    {
                        ChangeState(HanzoState.SpecialAction2);
                    }
                    else
                    {
                        int randProb = Random.Range(0, 10);
                        if(randProb>0)//aqui va la probablifdad de hacer el ataque tocho
                        {
                            ChangeState(HanzoState.SpecialAction);
                        }
                    }
                }


                timeAttack -= Time.deltaTime;
            }
            

            yield return null;
        }
        ChangeState(HanzoState.Following);
        yield break;
    }
    IEnumerator FollowingPlayer()
    {
        isPlayerClose = true;
        while(DistanceToPlayer() < longAttackDistance - (longAttackDistance / 3))
        {
            FollowPlayer();
            yield return null;
        }
        ChangeState(HanzoState.Following);
        yield break;
    }

    #endregion

    #region Ataques
    IEnumerator OnLongAttackState()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("longAttack");//tiempo total de animacion 1s

        yield return new WaitForSeconds(0.4f);
        longAttackCollider.enabled = true;
        longAttackCollider.size = new Vector2(0, longAttackCollider.size.y);
        longAttackCollider.size = new Vector2(2.2f, longAttackCollider.size.y);

        yield return new WaitForSeconds(0.1f);
        longAttackCollider.enabled = false;

        yield return new WaitForSeconds(0.6f);
        ChangeState(HanzoState.Following);


        yield break;
    }

    IEnumerator OnTripleAttackState()
    {
        animator.SetTrigger("specialAttack");

        while(DistanceToPlayer()<0.03f)
        {
            MoveToPoint(player.position, specialAttackSpeed);
            yield return null;
        }
        animator.SetTrigger("tripleAttack");
        yield return new WaitForSeconds(0.55f);
        Time.timeScale = 0;
    }
    #endregion
}
