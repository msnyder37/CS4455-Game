using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private ButtonScript b0;
    private ButtonScript b1;
    private ButtonScript b2;
    private ButtonScript b3;
    private ButtonScript b4;
    private ButtonScript b5;
    private ButtonScript b6;
    private MeshRenderer mr;
    
    private int size = 7;
    // Start is called before the first frame update
    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = false;
        b0 = GameObject.Find("Level3Button (0)").GetComponent<ButtonScript>();
        b1 = GameObject.Find("Level3Button (1)").GetComponent<ButtonScript>();
        b2 = GameObject.Find("Level3Button (2)").GetComponent<ButtonScript>();
        b3 = GameObject.Find("Level3Button (3)").GetComponent<ButtonScript>();
        b4 = GameObject.Find("Level3Button (4)").GetComponent<ButtonScript>();
        b5 = GameObject.Find("Level3Button (5)").GetComponent<ButtonScript>();
        b6 = GameObject.Find("Level3Button (6)").GetComponent<ButtonScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (b0.isPressed)
        {
            mr.enabled = true;
        }
    }
}
