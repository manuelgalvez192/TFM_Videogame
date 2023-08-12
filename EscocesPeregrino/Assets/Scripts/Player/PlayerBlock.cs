using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{


    private bool isBlocking;
    private bool canBlock;
    // Start is called before the first frame update
    void Start()
    {
        isBlocking = false;
        canBlock = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void BlockAttack()
    {
        if(canBlock)
        {
            //animacion de bloqueo

        }
    }


}
