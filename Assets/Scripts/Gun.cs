using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType {Semi, Burst, Auto};
    public GunType gunType;
	public Transform spawn;

    public void Shoot() {
    	Ray ray = new Ray(spawn.position, spawn.forward);
    	RaycastHit hit;

    	float shootDistance = 20.0f;

    	if (Physics.Raycast(ray, out hit, shootDistance)) {
    		shootDistance = hit.distance;
    	}

    	Debug.DrawRay(ray.origin, ray.direction * shootDistance, Color.red, 1);
    }

    public void ShootContinuous() {
        if (gunType == GunType.Auto) {
            Shoot();
        }
    }
}
