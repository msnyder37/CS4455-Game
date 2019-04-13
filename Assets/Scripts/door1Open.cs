using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door1Open : MonoBehaviour
{
    public Animator animator;
    public RobotHeroController playerScript;
    public Text HintText;
    public ButtonScript bs;

    private bool TextVisible;
    private float timer;

    void Start(){
        HintText.text = "";
        TextVisible = false;
        timer = 6f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bs = GameObject.Find("Level1Button").GetComponent<ButtonScript>();
            if (bs.isPressed)
            {
                //open
                animator.enabled = true;
                GetComponent<AudioSource>().Play();
            } else
            {
                TextVisible = true;
                HintText.text = "Access Denied. Press the red button first";
            }
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