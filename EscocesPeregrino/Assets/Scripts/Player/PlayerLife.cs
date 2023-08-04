using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    [SerializeField] private Slider playerLife;
    
    void Start()
    {
        playerLife.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
