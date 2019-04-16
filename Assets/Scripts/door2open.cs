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
        if (other.gameObject.tag == "Player" && playerScript.hasKey2 == true)
        {
            EventManager.TriggerEvent<DoorOpeningEvent, GameObject>(transform.gameObject);
            animator.enabled = true;
            door_open_audio.Play();
            //GetComponent<AudioSource>().Play();
        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey2 == false){
            access_denied_audio.Play();
            //TextVisible = true;
            //HintText.text = "Access Denied";
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