using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlsScript : MonoBehaviour
{
    [SerializeField] Transform notCurrentParent;

    public void ChangeControlsPage(GameObject _panel)
    {
        transform.GetChild(1).SetParent(notCurrentParent);
        _panel.transform.SetParent(this.transform);
    }
}
