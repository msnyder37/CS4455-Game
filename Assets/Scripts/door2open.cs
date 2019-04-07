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
    private float timer;

    void Start(){
        HintText.text = "";
        HintText.color = Color.red;
        TextVisible = false;
        timer = 1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerScript.hasKey2 == true)
        {
            animator.enabled = true;
            GetComponent<AudioSource>().Play();
        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey2 == false){
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