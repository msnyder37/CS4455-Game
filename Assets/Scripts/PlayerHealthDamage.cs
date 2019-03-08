using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to any object that you would like to inflict damage on the player once colliders touch. Adjust public damage_amount float from the Inspector (between 0 and 1 where 1 is an instant kill)
public class PlayerHealthDamage : MonoBehaviour
{
    GameObject thePlayer;
    PlayerHealth PlayerHealth;
    public float damage_amount = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Soldier");
        PlayerHealth = thePlayer.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.health_player -= damage_amount;
        }
    }
}
