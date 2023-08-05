using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject joyStick;



    //Funcionality
    bool isPadActive = false;

    [SerializeField] float horizontalAxis;
    [SerializeField] float verticalAxis;

    //gettersSetters
    public bool IsPadActive
    {
        get { return isPadActive; }
    }


    public float HorizontalAxis
    {
        get { return horizontalAxis; }
        set { horizontalAxis = value; }
    }
    public float VerticalAxis
    {
        get { return verticalAxis; }
        set { verticalAxis = value; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPadActive = true;

        //jpystick things
        joyStick.SetActive(true);
        joyStick.transform.position = Input.mousePosition;
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPadActive = false;
        joyStick.SetActive(false);
    }

    void ResetValues()//Aqui reseteamops todos los valores
    {
        horizontalAxis = 0;
        verticalAxis = 0;
    }
}
