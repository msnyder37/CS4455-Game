using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	//public Transform movingPlatform;
	public Transform pos1;
	public Transform pos2;
	public float speed;
	public float resetTime;

	private Vector3 newPos;
	private string state;

    void Start() {
    	ChangeTarget();
    }

    void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
    }

    void ChangeTarget() {
    	if (state == "pos1") {
    		state = "pos2";
    		newPos = pos2.position;
    	} else if (state == "pos2") {
    		state = "pos1";
    		newPos = pos1.position;
    	} else if (state == null) {
    		state = "pos2";
    		newPos = pos2.position;
    	}
    	Invoke("ChangeTarget", resetTime);
    }
}
