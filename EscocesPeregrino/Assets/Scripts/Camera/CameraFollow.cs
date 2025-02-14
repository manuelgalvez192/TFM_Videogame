using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minPlayerPosition;
    [SerializeField] private float maxPlayerPosition;
    
    public Vector3 offset;
    private void Start()
    {
        player =PlayerSingleton.instance.transform;
    }

    void FixedUpdate()
    {
        if (player.position.x > minPlayerPosition && player.position.x < maxPlayerPosition)
        {
            Vector3 desiredPosition = player.position + offset;

            // Solo actualizamos la posición en el eje X, manteniendo el valor actual en el eje Y
            desiredPosition.y = transform.position.y;
            desiredPosition.z = transform.position.z;

            // Actualizamos directamente la posición de la cámara
            transform.position = desiredPosition;
        }
    }
}
