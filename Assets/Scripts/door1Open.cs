﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door1Open : MonoBehaviour
{
    public Animator animator;
    public RobotHeroController playerScript;
    public Text HintText;

    private bool TextVisible;
    private bool doorOpened;
    private float timer;

    void Start(){
        HintText.text = "";
        HintText.color = Color.red;
        TextVisible = false;
        timer = 1f;
        doorOpened = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerScript.hasKey1 == true)
        {
            animator.enabled = true;
            EventManager.TriggerEvent<DoorOpeningEvent, GameObject>(transform.gameObject);
            doorOpened = true;
            playerScript.hasKey1 = false;
        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey1 == false && !doorOpened){
            TextVisible = true;
            HintText.text = "Access Denied";
        }
    }

    void Update(){
        if (TextVisible == true){
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && TextVisible == true){
            TextVisible = false;
            HintText.text = "";
            timer = 6f;
        }
    }
}