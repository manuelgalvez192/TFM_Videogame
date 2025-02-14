using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControlsScript : MonoBehaviour
{
    [SerializeField] Transform notCurrentParent;
    [SerializeField] GameObject cross;

    private void OnEnable()
    {
        cross.SetActive(InputsGameManager.instance.virtualPadEnabled);
    }
    public void ChangeControlsPage(GameObject _panel)
    {
        transform.GetChild(1).SetParent(notCurrentParent);
        _panel.transform.SetParent(this.transform);
    }
    public void ChangePadEnabled()
    {
        InputsGameManager.instance.virtualPadEnabled = !InputsGameManager.instance.virtualPadEnabled;
        cross.SetActive(InputsGameManager.instance.virtualPadEnabled);
    }
}
