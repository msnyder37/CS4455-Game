using System.Collections;
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

    AudioSource door_open_audio;
    AudioSource access_denied_audio;

    void Start(){
        HintText.text = "";
        HintText.color = Color.red;
        TextVisible = false;
        timer = 1f;
        AudioSource[] audios = GetComponents<AudioSource>();
        door_open_audio = audios[0];
        access_denied_audio = audios[1];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerScript.hasKey1 == true)
        {
            animator.enabled = true;
            //GetComponent<AudioSource>().Play();
            door_open_audio.Play();
        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey1 == false){
            //TextVisible = true;
            //HintText.text = "Access Denied";
            access_denied_audio.Play();
            //GetComponent<AudioSource>().Play();
            //AudioSource.PlayClipAtPoint(other.gameObject.GetComponent<AudioSource>().clip, GameObject.Find("Main Camera").transform.position);
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