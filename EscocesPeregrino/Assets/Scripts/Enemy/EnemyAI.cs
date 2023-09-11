using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float offset;
    

    private float detectionDistance;
    private Vector2 placeToGo;
    public bool canFollow = true;


    private void Start()
    {
        player = PlayerSingleton.instance.gameObject;
    }


    void FixedUpdate()
    {
        if (canFollow)
        {
            animator.SetBool("isWalking", false);
        
            detectionDistance = Vector2.Distance(transform.position, player.transform.position);

            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (detectionDistance <= detectionRange && detectionDistance > offset)
            {
                placeToGo = new Vector2(player.transform.position.x + offset, player.transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, placeToGo, speed * Time.deltaTime);
            
                animator.SetBool("isWalking", true);
            }

            if (rb.velocity.x > speed * Time.deltaTime)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);
            }
        }
    }
    public void ResetEnemy()
    {
        canFollow = true;
    }
    public void StopBehaviour()
    {
        canFollow = false;
    }

    public void ResetEnemyMovement()
    {
        canFollow = true;
    }
}
