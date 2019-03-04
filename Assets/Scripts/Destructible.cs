using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	public float destructibleHealth;

	void start(){
		destructibleHealth = 1.0f;
	}

    void OnTriggerEnter(Collider c) {
    	//Debug.Log(c.gameObject);
    	if (c.gameObject.CompareTag("Bullet")) {
    		// TODO: Play destruction animation
    		//Destroy(gameObject);
			destructibleHealth = destructibleHealth - 0.3f;
    	}

		if (destructibleHealth <= 0) {
			Destroy(gameObject);
		}
    }
}
