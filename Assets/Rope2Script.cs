using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope2Script : MonoBehaviour
{
    RopeScript rope1;
    public bool touch;
    float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        touch = rope1.touch;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!rope1.getTouch())
        {
            transform.Translate(0, Time.deltaTime * speed, 0);
        } else
        {
            //transform.Translate(0, Time.deltaTime * speed, 0);
        }
            
    }

    
}
