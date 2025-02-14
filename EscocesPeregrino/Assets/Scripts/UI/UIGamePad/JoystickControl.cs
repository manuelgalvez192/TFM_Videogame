using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickControl : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Image lastHandle;
    [SerializeField] Image nextCircle;
    [SerializeField] float maxDistance = 80;
    float currentDistance;
    [Header("Funcionality")]
    [SerializeField]UIPadController controller;
    [SerializeField] float responseSpeed = 10;

    void Update()
    {
        MoveHandle();
        SetAxis();
    }

    void MoveHandle()
    {
        Vector2 fingerPos = Input.mousePosition;

        lastHandle.rectTransform.position = Input.mousePosition;
        currentDistance = lastHandle.rectTransform.localPosition.magnitude;

        Vector2 newPos = lastHandle.rectTransform.localPosition;
        newPos.Normalize();

        nextCircle.rectTransform.localPosition = newPos * currentDistance / 2;

        if (currentDistance > maxDistance)
        {
            newPos *= maxDistance;
            lastHandle.rectTransform.localPosition = newPos;
            nextCircle.rectTransform.localPosition = newPos / 2;
        }
    }
    void SetAxis()
    {
        float hor = GetAxisMagnitude(lastHandle.rectTransform.localPosition.x);
        float ver = GetAxisMagnitude(lastHandle.rectTransform.localPosition.y);
        controller.HorizontalAxis= Mathf.Lerp(controller.HorizontalAxis, hor, responseSpeed * Time.deltaTime);
        controller.VerticalAxis = Mathf.Lerp(controller.VerticalAxis, ver, responseSpeed * Time.deltaTime);

        //controller.HorizontalAxis = GetAxisMagnitude(lastHandle.rectTransform.localPosition.x);
        //controller.VerticalAxis = GetAxisMagnitude(lastHandle.rectTransform.localPosition.y);
        
    }

    float GetAxisMagnitude(float magnitude)
    {
        return magnitude/ maxDistance;
    }
    
}
