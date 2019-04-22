using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	public float cooldown;

	private float clock;
	private bool emitting;
	private BoxCollider collider;
	private ParticleSystem emitter;

    void Start() {
        emitter = GetComponent<ParticleSystem>();
        collider = GetComponent<BoxCollider>();
        clock = cooldown;
        emitting = true;
    }

    void Update() {
        clock -= Time.deltaTime;
        if (clock < 0) {
        	clock = cooldown;
        	if (emitting) {
        		emitter.Stop();
        		collider.enabled = false;
        	} else {
        		emitter.Play();
        		collider.enabled = true;
        	}
        	emitting = !emitting;
        }
    }
}
