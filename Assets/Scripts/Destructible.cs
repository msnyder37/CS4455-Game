using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	public float destructibleHealth;
	public GameObject animation;
	public float animDuration;

	/*private float cooldown;
	private GameObject inst;

	void Start() {
		cooldown = 0.0f;
	}

	void Update() {
		if (cooldown < 0) {
			if (inst != null) {
				//Destroy(inst);
			}
		}
		cooldown -= Time.deltaTime;
	}*/

    void OnTriggerEnter(Collider c) {
    	if (c.gameObject.CompareTag("Bullet")) {
			destructibleHealth = destructibleHealth - 1.0f;
			if (destructibleHealth <= 0) {
				Explode();
			}
    	}
    }

    void Explode() {
    	GameObject obj = Instantiate(animation, transform.position, transform.rotation);
    	Destroy(this.gameObject);
    	Destroy(obj, animDuration);
    }
}
