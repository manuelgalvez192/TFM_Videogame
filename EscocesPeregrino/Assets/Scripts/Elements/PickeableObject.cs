using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(SpriteRenderer))]
public class PickeableObject : MonoBehaviour
{
    public bool canPick;
    public bool isPicked;
    Material objMat;
    [Header("Outliner Settings")]
    [SerializeField] float outlinerSpeed;
    [SerializeField] Color outlinerColor;
    [SerializeField] float maxOutliner;
    [SerializeField] float minOutliner;
    public virtual void Start()
    {
        objMat = GetComponent<SpriteRenderer>().material;
        objMat.SetTexture("Texture", GetComponent<SpriteRenderer>().sprite.texture);
        objMat.SetColor("_OutColor", outlinerColor);
    }
    private void Update()
    {
        if(canPick)
        {
            if(!isPicked)
            {
                StartCoroutine(MaterialOnSelected());
            }
            isPicked = true;
        }
    }
    public virtual void OnPickObject()
    {

    }
    public virtual void OnDropObject()
    {

    }

    IEnumerator MaterialOnSelected()
    {
        float x = 0;
        while(canPick)
        {
            x += outlinerSpeed * Time.deltaTime;
            float y = Mathf.Sin(x);
            objMat.SetFloat("_OutValue", y * (maxOutliner-minOutliner) + minOutliner);
            yield return null;
        }
        objMat.SetFloat("_OutValue", 0);
        isPicked = false;
        yield break;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canPick = true;
            StartCoroutine(MaterialOnSelected());
        }
    }

}

