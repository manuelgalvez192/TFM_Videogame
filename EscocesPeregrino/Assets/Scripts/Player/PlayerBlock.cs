using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{

    PlayerMovement playerMovement;
    PlayerLife playerLife;
    [SerializeField] Animator anim;
    
    private bool canBlock;
    private float timeToBlockAnim = 0.15f;

    bool wasBlocking = false;
    public bool allowedToRun = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!allowedToRun)
            return;
        canBlock = true;
    
        playerMovement= GetComponent<PlayerMovement>();
        playerLife= PlayerSingleton.instance.playerLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputsGameManager.instance.CoverButton)
        {
            if(!wasBlocking)
            {
                wasBlocking = true;
                BlockAttack();
            }
        }
        else
        {
            if(wasBlocking)
            {
                wasBlocking = false;
                playerMovement.isBlocking = false;
                playerLife.isBlocking = false;
                timeToBlockAnim = 0.15f;
                anim.speed = 1;
                anim.SetBool("isBlocking", false);
            }
        }
        //if(InputsGameManager.instance.CoverButtonDown)
        //{
        //    BlockAttack();
        //}
        //else if(InputsGameManager.instance.CoverButtonUp)
        //{
        //    playerMovement.isBlocking = false;
        //    playerLife.isBlocking = false;
        //    timeToBlockAnim = 0.15f;
        //    anim.speed = 1;
        //    anim.SetBool("isBlocking", false);
        //}
    }


    private void BlockAttack()
    {
        if(canBlock)
        {
            timeToBlockAnim -= Time.deltaTime;
            playerMovement.isBlocking = true;
            playerLife.isBlocking = true;
            anim.SetBool("isBlocking", true);

            if (timeToBlockAnim <= 0)
            {
                anim.speed = 0;
            }
            
 

        }
    }


}
