using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door3open : MonoBehaviour
{
    public Animator animator;
    public RobotHeroController playerScript;
    public Text HintText;

    private bool TextVisible;
    private bool doorOpened;
    private float timer;

    void Start(){
        HintText.text = "";
        TextVisible = false;
        timer = 6f;
        doorOpened = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerScript.hasKey3 == true)
        {
            animator.enabled = true;
            EventManager.TriggerEvent<DoorOpeningEvent, GameObject>(transform.gameObject);
            playerScript.hasKey3 = false;
            doorOpened = true;
        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey3 == false && !doorOpened){
            TextVisible = true;
            HintText.text = "Another access card needed?! I sure hope I won't have to jump across these platforms again!";
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