using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door2open : MonoBehaviour
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
        if (other.gameObject.tag == "Player" && playerScript.hasKey2 == true)
        {
            EventManager.TriggerEvent<DoorOpeningEvent, GameObject>(transform.gameObject);
            animator.enabled = true;
            doorOpened = true;
            playerScript.hasKey2 = false;
        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey2 == false && !doorOpened){
            TextVisible = true;
            HintText.text = "Hmm.. yet another access card is required. Perhaps, I should look for it...";
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