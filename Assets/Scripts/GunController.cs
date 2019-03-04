using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Ammo Tracking and Display - Bishoy
    public int ammo = 100;
    public Text ammoDisplay;  

    //Display ammo for HUD - Bishoy
    void Update() {
        ammoDisplay.text = ammo.ToString();
    }

    public void Shoot() {
        if (ammo > 0){  //Only fire if we have ammo - Bishoy
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

                    //Keep track of ammo - Bishoy
                    ammo--;
                }
            } else {
                shotClock = 0;
        }
        }
            
    }

    public void ShootContinuous() {
        if (gunType == GunType.Auto) {
            Shoot();
        }
    }
}
