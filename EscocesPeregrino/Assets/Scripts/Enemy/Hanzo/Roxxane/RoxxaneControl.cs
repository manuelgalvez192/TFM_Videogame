using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoxxaneControl : BaseHanzo
{
    [SerializeField] float longAttackDistance;
    IEnumerator currentFollowingCorroutine;
    bool isPlayerClose = false;
    [SerializeField] BoxCollider2D longAttackCollider;
    [SerializeField] BoxCollider2D damageCollision;
    [SerializeField] float specialAttackSpeed;
    [SerializeField] SpriteRenderer trunk;
    void Start()
    {
        base.Start();
        trunk.transform.parent = null;
        trunk.gameObject.SetActive(false);
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
                currentCorroutine = OnDamageState();
                break;
            case HanzoState.Attacking:
                currentCorroutine = OnAttackingState();
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
                    if(Mathf.Abs(newPos.y - transform.position.y) < 0.1f)
                    {
                        ChangeState(HanzoState.SpecialAction2);
                    }
                    else
                    {
                        int randProb = Random.Range(0, 10);
                        if(randProb>7)//aqui va la probablifdad de hacer el ataque tocho
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
            if(DistanceToPlayer()<attackDistance)
            {
                 ChangeState(HanzoState.Attacking);
            }
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
        while (true)
            yield return null;
        yield break;
    }
    IEnumerator OnLongAttackState44()
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
        Quaternion currentRot = transform.rotation;
        while (DistanceToPlayer() > 0.25f)
        {
            MoveToPoint(player.position, specialAttackSpeed);
            yield return null;
        }
        animator.SetTrigger("tripleAttack");
        float nexTime = 0.65f;
        while (nexTime >= 0)
        {
            nexTime -= Time.deltaTime;
            MoveToPoint(player.position, 0.5f);
            transform.rotation = currentRot;
            yield return null;
        }
        //damageCollision.enabled = false;
        //yield return new WaitForSeconds(0.45f);
        //StartCoroutine(HidingTrunk());
        //transform.position = new Vector2(longAttackDistance, transform.position.y) + (Vector2)player.position;
        //ChangeState(HanzoState.Following);
    }
    IEnumerator OnTripleAttackState44()
    {
        animator.SetTrigger("specialAttack");
        Quaternion currentRot = transform.rotation;
        while(DistanceToPlayer()> 0.25f)
        {
            MoveToPoint(player.position, specialAttackSpeed);
            yield return null;
        }
        animator.SetTrigger("tripleAttack");
        float nexTime = 0.65f;
        while(nexTime>=0)
        {
            nexTime -= Time.deltaTime;
            MoveToPoint(player.position, 0.5f);
            transform.rotation = currentRot;
            yield return null;
        }
        damageCollision.enabled = false;
        yield return new WaitForSeconds(0.45f);
        StartCoroutine(HidingTrunk());
        transform.position = new Vector2(longAttackDistance,transform.position.y)+(Vector2)player.position;
        ChangeState(HanzoState.Following);
    }
    IEnumerator HidingTrunk()
    {
        trunk.transform.position = transform.position;
        trunk.transform.rotation = transform.rotation;
        trunk.gameObject.SetActive(true);
        trunk.color = new Color(trunk.color.r, trunk.color.g, trunk.color.b, 1);

        yield return new WaitForSeconds(1);
        float alpha = 1;
        while(alpha >0)
        {
            alpha -= Time.deltaTime;
            trunk.color = new Color(trunk.color.r, trunk.color.g, trunk.color.b, alpha);
            yield return null;
        }
        trunk.gameObject.SetActive(false);
        yield break;
    }
    IEnumerator OnAttackingState()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackRate);

        ChangeState(DistanceToPlayer() < attackDistance ? HanzoState.Attacking : HanzoState.Waiting);
    }
    public void ActiveAttackCollision()
    {
        attackCollision.enabled = true;
        attackCollision.size = new Vector2(0, longAttackCollider.size.y);
        attackCollision.size = new Vector2(0.41f, longAttackCollider.size.y);
    }
    public void DeactiveAttackCollision()
    {
        attackCollision.enabled = false;
    }
    public void ActiveLongAttackCollision()
    {
        longAttackCollider.enabled = true;
        longAttackCollider.size = new Vector2(0, longAttackCollider.size.y);
        longAttackCollider.size = new Vector2(2.2f, longAttackCollider.size.y);
    }
    public void DeactiveLongAttackCollision()
    {
        longAttackCollider.enabled = false;
    }
    public void TPOnAnimation()
    {
        StartCoroutine(HidingTrunk());
        transform.position = new Vector2(longAttackDistance, transform.position.y) + (Vector2)player.position;
    }
    public void FinishAttack()
    {
        ChangeState(HanzoState.Following);
    }
    #endregion
    public override void GetDamage()
    {
        StopCoroutine(currentCorroutine);
        base.GetDamage();
        ChangeState(HanzoState.GettingDamage);
    }
    IEnumerator OnDamageState()
    {
        animator.SetTrigger("damage");
        yield return new WaitForSeconds(0.9f);
        ChangeState(HanzoState.Waiting);

        yield break;
    }

    public override void Die()
    {
        base.Die();
        StopCoroutine(currentCorroutine);
        animator.SetTrigger("die");
    }
}
