using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	public Transform start;
	public Transform end;
	public float speed;

	private Vector3 target;
	private bool towardsEnd;
	private Rigidbody rb;
	private float distance;

	void Start() {
		//target = end;
		towardsEnd = true;
		rb = GetComponent<Rigidbody>();
		target = GetDirection(transform, end);
		//direction = GetDirection(start, end);
	}

	void FixedUpdate() {
		distance = Vector3.Distance(transform.position, target);
		if (distance > .1) {
			Move(transform.position, target);
		} else {
			if (!towardsEnd) {
				towardsEnd = true;
				target = GetDirection(transform, end);
			} else {
				towardsEnd = false;
				target = GetDirection(transform, start);
			}
		}
	}

	void Move(Vector3 pos, Vector3 dest) {
		Vector3 direction = (dest-pos).normalized;
		rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
		//transform.position = Vector3.MoveTowards(pos, dest, speed);
	}

	Vector3 GetDirection(Transform s, Transform e) {
    	return new Vector3(e.position.x-s.position.x, e.position.y-s.position.y, e.position.z-s.position.z);
    }

    /*void Update() {
    	if (Vector3.Distance(transform.position, target.position) <= 1) {
    		Debug.Log("Hit");
    		if (target == start) {
    			target = end;
    			direction = GetDirection(start, end);
    		} else {
    			target = start;
    			direction = GetDirection(end, start);
    		}
    	}

    	transform.position = Vector3.MoveTowards(transform.position, target.position, .1f);

    	//rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    Vector3 GetDirection(Transform s, Transform e) {
    	return new Vector3(e.position.x-s.position.x, e.position.y-s.position.y, e.position.z-s.position.z);
    }*/
}
