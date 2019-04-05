using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to any object that you would like to inflict damage on the player once colliders touch. Adjust public damage_amount float from the Inspector (between 0 and 1 where 1 is an instant kill)
public class PlayerHealthDamage : MonoBehaviour
{
    GameObject thePlayer;
    PlayerHealth PlayerHealth;
    public float damage_amount = 0.1f;
    public float cooldown = 2.0f;

    private float clock;

    void Start() {
        thePlayer = GameObject.Find("Soldier");
        PlayerHealth = thePlayer.GetComponent<PlayerHealth>();

        clock = cooldown;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerHealth.health_player -= damage_amount;
            EventManager.TriggerEvent<PlayerDamageEvent, PlayerHealthDamage>(this);
        }
    }

    void OnTriggerStay(Collider other) {
        clock -= Time.deltaTime;
        if (other.gameObject.tag == "Player") {
            if (clock < 0) {
                clock = cooldown;
                PlayerHealth.health_player -= damage_amount;
                EventManager.TriggerEvent<PlayerDamageEvent, PlayerHealthDamage>(this);
            }
        }
    }
}
