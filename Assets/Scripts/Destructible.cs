using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    void OnTriggerEnter(Collider c) {
    	//Debug.Log(c.gameObject);
    	if (c.gameObject.CompareTag("Bullet")) {
    		// TODO: Play destruction animation
    		//Destroy(gameObject);
    	}
    }
}
