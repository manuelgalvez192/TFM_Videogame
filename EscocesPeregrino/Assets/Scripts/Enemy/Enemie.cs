using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    static Vector2 LimitsY = new Vector2(0f,2f);
    enum States { patrol, pursuit};

    [SerializeField]
    private float verticalSpeed;
    [SerializeField]    
    float horizontalSpeed;


    [SerializeField]
    private float maxLife = 100f;

    private float currentLife;



    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    [SerializeField]
    float searchRange = 1;
    [SerializeField]
    float stoppingDistance = 0.3f;

    [SerializeField]
    States state = States.patrol;

    [SerializeField]
    protected Transform rightPunch;
    [SerializeField]
    protected Transform leftPunch;
    [SerializeField]
    protected float punchRadius=0.1f;

    Transform player;
    Vector3 target;
    Vector2 vel;

    bool isFirstStunned;
    bool isStunned;

  
    
  

    private Vector3 originalPos;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPos= transform.position;
        currentLife = maxLife;
        InvokeRepeating("SetTarget", 0, 5);
        InvokeRepeating("Punch", 0, 5);
        isFirstStunned = false;
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Die();
        if (state == States.pursuit)
        {
            target = player.transform.position;
            if(Vector3.Distance(target,transform.position) > searchRange * 1.2f)
            {
                target= transform.position;
                state= States.patrol;
                return;
            }
        } 

        else if(state== States.patrol)
        {
           var circle = Physics2D.CircleCast(transform.position, searchRange, Vector2.up);
            if(circle.collider !=null)
            {
                if(circle.collider.CompareTag("Player"))
                {
                    state= States.pursuit;

                }
            }
        }

       /* if(currentLife <=50 && !isFirstStunned)
        {
            isFirstStunned = true;

            if(isFirstStunned)
            {
               // vel = Vector2.zero;
                anim.SetTrigger("isFlying");
                Vector2 dir = this.transform.position - player.transform.position;
                dir.Normalize();
                float strength = 10f;
                rb.AddForce(dir * strength, ForceMode2D.Impulse);
                Debug.Log("aaa");
                isStunned = true;
                
            }
        }

        if(isStunned)
        {
            anim.SetBool("isKnocked",true);
        }*/
      
    }

    private  void OnDrawGizmosSelected()
    {
        if (rightPunch == null || leftPunch == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(leftPunch.position, punchRadius);
        Gizmos.DrawWireSphere(rightPunch.position, punchRadius);
       

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.DrawWireSphere(target, 0.2f);

      
    }
   
    private void SetTarget()
    {
        if (state != States.patrol)
            return;
        target = new Vector2(transform.position.x + Random.Range(-searchRange, searchRange), Random.Range(LimitsY.y, LimitsY.x));
    }

    private void Movement()
    {
       
        vel= target - transform.position;

        if (vel.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if(vel.magnitude < stoppingDistance)
        {
            vel = Vector2.zero;
        }
        vel.Normalize();
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemie_Punch1") || !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemie_Hitted"))
       {
            if (state == States.patrol)
            {
                anim.SetBool("IsWalking", vel.magnitude != 0);
                anim.SetBool("IsChasing", false);
            }
            else if (state == States.pursuit)
            {
                anim.SetBool("IsChasing", vel.magnitude!=0);
            }
            
       }
       else
       {
           vel = Vector2.zero;
       }
       
        rb.velocity = new Vector2(vel.x * horizontalSpeed, vel.y * verticalSpeed);
    }

    void Punch()
    {
        if (state != States.pursuit)
            return;
        if (vel.magnitude != 0)
            return;
 
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemie_Punch1"))
        {
            anim.SetTrigger("Punch");
        }
    }

    void RecieveDamage(float damage)
    {
        currentLife -= damage;
        
        
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RecieveDamage(5);
            anim.SetTrigger("Hitted");
            vel = Vector2.zero;
        }
    }

    private void Die()
    {
        if(currentLife<= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
 
}
