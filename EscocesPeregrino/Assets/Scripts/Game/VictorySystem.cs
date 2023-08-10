using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySystem : MonoBehaviour
{
    float duration = 10;
    Material visionShader;
    IEnumerator Win()
    {
        float currentTime = duration;
        while(true)
        {

            yield return null;
        }

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            StartCoroutine("Win");
        }
    }
}
