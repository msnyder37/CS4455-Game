using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PulleyScript : MonoBehaviour
{
    Rigidbody rigid;
    float speed = -1.5f;
    bool touch;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        touch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!touch)
        transform.Translate(0, Time.deltaTime * speed, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        //transform.Translate(0, Time.deltaTime * speed, 0);
        if (other.gameObject.tag == "Destructible")
        {
            touch = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Destructible")
        {
            touch = false;
        }
    }
}
