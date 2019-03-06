using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	public int health;
    void OnTriggerEnter(Collider c) {
    	//Debug.Log(c.gameObject);
    	if (c.gameObject.CompareTag("Bullet")) {
    		// TODO: Play destruction animation
    		health--;
    		if (health <= 0) {
    			Destroy(transform.gameObject);

    		}
    	}
    }
}
