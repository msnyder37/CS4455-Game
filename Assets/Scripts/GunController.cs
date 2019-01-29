using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public enum GunType {Semi, Burst, Auto};
    public GunType gunType;
    public BulletController bullet;
	public Transform spawn;
    public float speed;
    public float cooldown;
    [HideInInspector] public bool isFiring;

    private float shotClock;

    public void Shoot() {
        if (isFiring) {
            shotClock -= Time.deltaTime;
            if (shotClock <= 0) {
                shotClock = cooldown;

            	//Ray ray = new Ray(spawn.position, spawn.forward);
            	//RaycastHit hit;

            	//float shootDistance = 20.0f;

            	//if (Physics.Raycast(ray, out hit, shootDistance)) {
            	//	shootDistance = hit.distance;
            	//}

            	//Debug.DrawRay(ray.origin, ray.direction * shootDistance, Color.red, 1);
                BulletController nb = Instantiate(bullet, spawn.position, spawn.rotation) as BulletController;
                nb.speed = speed;
            }
        } else {
            shotClock = 0;
        }
    }

    public void ShootContinuous() {
        if (gunType == GunType.Auto) {
            Shoot();
        }
    }
}
