using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        // Solo actualizamos la posición en el eje X, manteniendo el valor actual en el eje Y
        desiredPosition.y = transform.position.y;
        desiredPosition.z = transform.position.z;

        // Actualizamos directamente la posición de la cámara
        transform.position = desiredPosition;
    }
}
