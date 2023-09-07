using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSound : MonoBehaviour
{
   public void ThrowStepSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFootsteps, this.transform.position);
    }
}