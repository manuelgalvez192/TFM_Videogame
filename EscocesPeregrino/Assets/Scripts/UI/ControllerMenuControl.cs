using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMenuControl : MonoBehaviour
{
    public bool controllerExist;

    private void Awake()
    {
        // Verifica si hay controladores conectados al inicio del juego
        CheckForControllers();
    }

    private void OnEnable()
    {
        // Suscribirse al evento de conexi�n de joystick
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        // Aseg�rate de desuscribirte al salir del script o del objeto que lo contiene
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added && device is Gamepad)
        {
            // Se ha conectado un joystick, puedes agregar aqu� la l�gica que desees
            Debug.Log("HolaSoyUnMando");
            controllerExist = true;
            // Agrega tu l�gica adicional aqu�
        }
        else if (change == InputDeviceChange.Removed && device is Gamepad)
        {
            // Se ha desconectado un joystick, puedes manejarlo aqu� si es necesario
            controllerExist = false;
        }
    }

    private void CheckForControllers()
    {
        // Comprueba si hay controladores conectados al inicio del juego
        var gamepads = Gamepad.all;
        if (gamepads.Count > 0)
        {
            controllerExist = true;
            Debug.Log("Controlador detectado al inicio del juego.");
        }
    }
}
