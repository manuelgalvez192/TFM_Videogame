 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BaseHanzo : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject attackCollision;

    protected Transform player;
    [SerializeField] protected float speed;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float offset;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float attackRate;

    bool allowedToMove = true;
    protected bool followingPlayer;

    protected enum HanzoState { Waiting, Following,CheckingLastPositon,GettingDamage, Attacking, SpecialAction }
    [SerializeField]protected HanzoState state = HanzoState.Waiting;
    protected IEnumerator currentCorroutine;

    protected void Start()
    {
        player = PlayerSingleton.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        attackCollision.SetActive(false);
        ChangeState(HanzoState.Waiting);
    }
    private void OnEnable()
    {
        animator.SetTrigger("Reset");
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
    protected virtual void ChangeState(HanzoState newState)
    {
        if(currentCorroutine!=null)
            StopCoroutine(currentCorroutine);
        state = newState;
    }
    protected virtual void Attack()
    {

    }
    public virtual void GetDamage()
    {

    }
    public virtual void Die()
    {

    }
    public virtual void StopBehaviour()
    {
        StopCoroutine(currentCorroutine);
        animator.speed = 0;
    }

}
