using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door1Open : MonoBehaviour
{
    public Animator animator;
    public RobotHeroController playerScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerScript.hasKey1 == true)
        {
            animator.enabled = true;
        }
    }
}