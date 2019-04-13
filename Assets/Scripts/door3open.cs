using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door3open : MonoBehaviour
{
    public bool door3Pressed;
    void Start(){
        door3Pressed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door3Pressed = true;
        }
    }

    
}