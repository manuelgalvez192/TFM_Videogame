 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BaseHanzo : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] protected float speed;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float offset;

    bool allowedToMove = true;
    Vector2 lastPlayerPosition;
    Vector2 lastEnemyPosition;
    void Start()
    {
        player = PlayerSingleton.instance.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (!allowedToMove)
        //    return;
        //
        if (DistanceToPlayer() < detectionRange)
            FollowPlayer();
    }
    
    protected void FollowPlayer()
    {
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
        if(DistanceToPlayer()>detectionRange)
            
        lastEnemyPosition = transform.position;
    }

    protected float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }
    protected virtual void Attack()
    {

    }
}
