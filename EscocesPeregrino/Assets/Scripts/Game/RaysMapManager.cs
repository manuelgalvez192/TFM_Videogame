using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysMapManager : MonoBehaviour
{
    public GameObject[] rays;
    bool isPlayerIn;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    PlayerMovement player;
    void Start()
    {
        rays = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            rays[i] = transform.GetChild(i).gameObject;
        }

    }

    IEnumerator Funcionality()
    {
        isPlayerIn = true;
        float randTime = 0;
        while(isPlayerIn)
        {
            randTime -= Time.deltaTime;
            if(randTime<= 0)
            {
                int rand = 0;
                foreach(GameObject obj in rays)
                {
                    rand = Random.Range(1, 6);
                    if(rand>2)
                    {
                        obj.SetActive(true);
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.laser, this.transform.position);
                    }  
                    else
                        obj.SetActive(false);
                }

                randTime = Random.Range(minTime, maxTime);
            }
            yield return null;
        }
        yield break;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine("Funcionality");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = false;
        }
    }
    public void PlayRayParticles()
    {
        PlayerSingleton.instance.PlayRayParticles();
    }
}

