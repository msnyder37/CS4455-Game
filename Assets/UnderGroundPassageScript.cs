using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGroundPassageScript : MonoBehaviour
{
    Animator anim;
    public door3open d3;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        //d3 = GameObject.Find("door3 control").GetComponent<door3open>();
    }

    // Update is called once per frame
    void Update()
    {
        d3 = GameObject.Find("door3 control").GetComponent<door3open>();
        if (d3.door3Pressed)
        {
            anim.enabled = true;
        }
    }

    /*
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Destructible")
        {
            startTime = Time.deltaTime;
            anim.enabled = true;
        }
        
        
    }
    */
}
