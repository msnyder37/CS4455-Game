using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour {

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Player") {
			c.GetComponent<Collider>().transform.SetParent(transform);
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.gameObject.tag == "Player") {
			c.GetComponent<Collider>().transform.SetParent(null);
		}		
	}
}
