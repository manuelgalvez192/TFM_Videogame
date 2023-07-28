using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementePruebas : MonoBehaviour
{
    public float speed = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}
