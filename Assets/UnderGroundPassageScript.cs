using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGroundPassageScript : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    float timer = 0.0f;
    int second;
    void Update()
    {
        timer += Time.deltaTime;
        second = (int)(timer);
        if (second > 10)
        {
            anim.enabled = false;
        }
        Debug.Log(timer);
    }

    bool stop = false;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("                                                " + other);
        if (!stop)
        {
            if (other.gameObject.tag == "Destructable")
            {
                anim.enabled = false;
            }
            else
            {
                anim.enabled = true;
                stop = true;
            }
        }
        
    }
    
}
