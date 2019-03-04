using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[HideInInspector] public float speed;

    void Update() {
    	transform.Translate(Vector3.forward * speed * Time.deltaTime);
    	Destroy(gameObject, 1);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
    }*/

    void OnTriggerEnter(Collider c) {
        Debug.Log("Hit");
        Debug.Log(c.gameObject.tag);
        if (c.gameObject.CompareTag("Destructible")) {
            //Destroy(c.gameObject);
            Destroy(gameObject);
        } else if (c.gameObject.CompareTag("Indestructible")) {
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
