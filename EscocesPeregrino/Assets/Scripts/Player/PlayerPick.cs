using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerPick : MonoBehaviour
{
    PickeableObject currentPickeable;
    [SerializeField] KeyCode PickInput;
    bool haveSomethingSelected = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    IEnumerator PickeableUpdate()
    {
        while(haveSomethingSelected)
        {
            if(Input.GetKeyDown(PickInput))
            {
                if (currentPickeable)
                {
                    currentPickeable.OnPickObject();
                    haveSomethingSelected = false;
                }
            }


            yield return null;
        }
        yield break;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pickeable")
        {
            if(currentPickeable)
                currentPickeable.UnSelect();

            currentPickeable = other.GetComponent<PickeableObject>();
            currentPickeable.Select();
            if (!haveSomethingSelected)
            {
                haveSomethingSelected = true;
                StartCoroutine(PickeableUpdate());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == currentPickeable.gameObject)
        {
            currentPickeable.UnSelect();
            haveSomethingSelected = false;
        }
    }
}
