using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door3open : MonoBehaviour
{
    public Animator animator;
    public RobotHeroController playerScript;
    public Text HintText;
    public bool door3Pressed;

    private bool TextVisible;
    private bool doorOpened;
    private float timer;

    AudioSource door_open_audio;
    AudioSource access_denied_audio;

    void Start()
    {
        HintText.text = "";
        HintText.color = Color.red;
        TextVisible = false;
        timer = 6f;
        AudioSource[] audios = GetComponents<AudioSource>();
        door_open_audio = audios[0];
        access_denied_audio = audios[1];
        door3Pressed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door3Pressed = true;
            animator.enabled = true;
            door_open_audio.Play();
            //GetComponent<AudioSource>().Play();

        }
        else if (other.gameObject.tag == "Player" && playerScript.hasKey3 == false)
        {
            access_denied_audio.Play();
            TextVisible = true;
            HintText.text = "Access Denied. Must have key-card.";
        }
    }

    void Update()
    {
        if (TextVisible == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && TextVisible == true)
        {
            TextVisible = false;
            HintText.text = "";
            timer = 6f;
        }
    }

}