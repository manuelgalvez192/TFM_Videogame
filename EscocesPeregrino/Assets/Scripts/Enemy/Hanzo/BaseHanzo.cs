 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BaseHanzo : MonoBehaviour
{
    protected Transform player;
    Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float speed;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float offset;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float attackRate;
    [SerializeField] protected GameObject attackCollision;

    bool allowedToMove = true;
    protected bool followingPlayer;

    protected enum HanzoState { Waiting, Following,CheckingLastPositon, Attacking, SpecialAction }
    [SerializeField]protected HanzoState state = HanzoState.Waiting;
    protected IEnumerator currentCorroutine;

    protected void Start()
    {
        player = PlayerSingleton.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        attackCollision.SetActive(false);
        ChangeState(HanzoState.Waiting);
    }

   
    
    protected void FollowPlayer()
    {
        //animator.SetBool("isMoving", true);
        Vector2 playerDir = player.position - transform.position;

        playerDir.Normalize();

        rb.MovePosition(rb.position + playerDir * speed * Time.deltaTime);
        if (playerDir.x < 0)
        {
            transform.rotation =  Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation =  Quaternion.Euler(0, 0, 0);
        }
            
    }
    protected void MoveToPoint(Vector2 target)
    {
        //animator.SetBool("isMoving", true);
        Vector2 playerDir = target - (Vector2)transform.position;

        playerDir.Normalize();

        rb.MovePosition(rb.position + playerDir * speed * Time.deltaTime);
        if (playerDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    protected float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }
    protected virtual void Attack()
    {

    }
    protected virtual void ChangeState(HanzoState newState)
    {
        if(currentCorroutine!=null)
            StopCoroutine(currentCorroutine);
        state = newState;
    }
}
