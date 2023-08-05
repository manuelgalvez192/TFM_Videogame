using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlsScript : MonoBehaviour
{
    [SerializeField] Transform notCurrentParent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeControlsPage(GameObject _panel)
    {
        transform.GetChild(1).SetParent(notCurrentParent);
        _panel.transform.SetParent(this.transform);
    }
}
