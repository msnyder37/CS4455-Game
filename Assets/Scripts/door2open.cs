using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door2open : MonoBehaviour
{
    public Animator animator;
    public RobotHeroController playerScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerScript.hasKey2 == true)
        {
            animator.enabled = true;
        }
    }
}