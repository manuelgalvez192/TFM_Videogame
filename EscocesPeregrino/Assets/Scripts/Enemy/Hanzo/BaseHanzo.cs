 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BaseHanzo : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected BoxCollider2D attackCollision;
    protected float attackCollisionSize;
    protected Transform player;
    [SerializeField] protected float speed;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float offset;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float attackRate;
    [SerializeField] Transform particlePos;
    [SerializeField] GameObject healthyObject;
    [SerializeField] GameEvent OnEnemyDie;

    protected bool allowedToMove = true;
    protected bool followingPlayer;

    protected enum HanzoState { Waiting, Following,CheckingLastPositon,GettingDamage, Attacking, SpecialAction, SpecialAction2 }
    [SerializeField]protected HanzoState state = HanzoState.Waiting;
    protected IEnumerator currentCorroutine;

    protected void Start()
    {
        player = PlayerSingleton.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        attackCollision.enabled = false;
        attackCollisionSize = attackCollision.size.x;
        ChangeState(HanzoState.Waiting);
    }
    private void OnEnable()
    {
        animator.SetTrigger("reset");
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
    protected void MoveToPoint(Vector2 target,bool canRotate = true)
    {
        Vector2 playerDir = target - (Vector2)transform.position;

        playerDir.Normalize();

        rb.MovePosition(rb.position + playerDir * speed * Time.deltaTime);
        if(canRotate)
        {
            if (playerDir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }

    }
    protected void MoveToPoint(Vector2 target, float voidSpeed)
    {
        Vector2 playerDir = target - (Vector2)transform.position;

        playerDir.Normalize();

        rb.MovePosition(rb.position + playerDir * voidSpeed * Time.deltaTime);

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
    //protected virtual void Attack()
    //{
    //
    //}
    public virtual void GetDamage()
    {

    }
    public virtual void Die()
    {
        allowedToMove = false;
        Invoke("ThrowDieParticles", 1.3f);
        Destroy(this.gameObject, 2.5f);
        OnEnemyDie.Raise();
    }
    void ThrowDieParticles()
    {
        ParticleSystemManager.instance.ThrowParticleSystem("Die", particlePos);
        int randHealDrop = UnityEngine.Random.Range(0, 3);
        if (randHealDrop > 1)
        {
            GameObject newDrop = Instantiate(healthyObject, transform.position, transform.rotation);//TO DO:: Si se hace pool camboiar el spawn
            newDrop.GetComponent<PickeableObject>().OnDropObject();
        }
    }
    public virtual void StopBehaviour()
    {
        StopCoroutine(currentCorroutine);
        animator.speed = 0;
    }
    public virtual void OnPlayerResucite()
    {
        ChangeState(HanzoState.Waiting);
        animator.SetTrigger("reset");
        animator.speed = 1;
        if(DistanceToPlayer()<=1.5)
        {
            rb.position = new Vector2(PlayerSingleton.instance.gameObject.transform.position.x + 3, rb.position.y);
        }
    }

}
