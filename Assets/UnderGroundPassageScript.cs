using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGroundPassageScript : MonoBehaviour
{
    Animator anim;
    float startTime = 0.0f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        anim = GetComponent<Animator>();
        anim.enabled = false;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (startTime != 0.0f)
        {
            timer = Time.deltaTime;
        }

        if (timer - 5.0f > startTime)
        {
            anim.enabled = false;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Destructible")
        {
            startTime = Time.deltaTime;
            anim.enabled = true;
        }
        
        
    }
}
