using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(SpriteRenderer))]
public class PickeableObject : MonoBehaviour
{
    public bool isPicked;
    public bool isSelected = false;
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
        objMat.SetFloat("_OutValue", 0);

    }

    public virtual void OnPickObject()
    {
        if (!isSelected)
            return;
    }
    public virtual void OnDropObject()
    {

    }

    public virtual void Select()
    {
        if (isSelected)
            return;
        isSelected = true;
        StartCoroutine(MaterialOnSelected());
    }

    public virtual void UnSelect()
    {
        isSelected = false;
    }

    IEnumerator MaterialOnSelected()
    {
        float x = 0;
        while(isSelected)
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

}

