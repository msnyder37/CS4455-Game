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
}
