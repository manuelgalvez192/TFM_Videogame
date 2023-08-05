using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPadButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool isPressed = false;
    bool hasBeenPressed = false;
    bool startPicking = false;

    float onUnpressedAlpha;
    Image img;
    UIPadController controllerPrent;
    public UIPadController ControllerParent
    { set { controllerPrent = value; } }
    private void Awake()
    {
        img = GetComponent<Image>();
        onUnpressedAlpha =img.color.a;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        controllerPrent.IsButtonsPadActive = true;
        isPressed = true;
        startPicking = true;
        img.color = new Vector4(img.color.r, img.color.g, img.color.b, 1);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        controllerPrent.IsButtonsPadActive = false;
        isPressed = false;
        hasBeenPressed = false;
        img.color = new Vector4(img.color.r, img.color.g, img.color.b, onUnpressedAlpha);
        //startPicking = false;
    }



    public bool GetPadButtonDown()
    {
        if (isPressed && !hasBeenPressed)
        {
            hasBeenPressed = true;
            return true;
        }
        
        return false;
    }
    public bool GetPadButton()
    {
        if (isPressed)
            hasBeenPressed = false;
        return isPressed;
    }
    public bool GetPadButtonUp()
    {
        if (startPicking && !isPressed)
        {
            startPicking = false;
            return true;
        }
        return false;
    }

}
