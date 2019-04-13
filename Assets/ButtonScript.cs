using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPressed;
    public Material[] materials;
    public MeshRenderer rend;
    public int index = 0;

    void Start()
    {
        isPressed = false;
        rend = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            isPressed = !isPressed;
            
            if (!isPressed)
            {
                rend.material = materials[0];

            } else
            {
                rend.material = materials[1];
            }         
        }
    }
}
