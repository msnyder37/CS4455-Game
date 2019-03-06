using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door3open : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerScript;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && playerScript.hasKey3 == true) {
            animator.enabled = true;
        }
    }
}