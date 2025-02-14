using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebasInputs : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(InputsGameManager.instance.AttackButtonDown)
        {
            print("Ataque");
        }
        if (InputsGameManager.instance.DashButtonDown)
        {
            print("dash");
        }
        if (InputsGameManager.instance.CoverButtonDown)
        {
            print("cover");
        }
        if (InputsGameManager.instance.PickButtonDown)
        {
            print("pick");
        }
        if (InputsGameManager.instance.JumpButtonDown)
        {
            print("jump");
        }

    }
}
