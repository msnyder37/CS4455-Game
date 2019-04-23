using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health_player = 0.30f; //1.00f;    //health is ranges between 0.00 to 1.00
    public Slider healthbar;
    public Animator animator;  
    private bool animation_started = false;

    
    void Update()
    {
        //The slider value always gets the value of the public float health_player
        healthbar.value = health_player;

        //If healthbar drops too low, load the game over screen
        if (health_player < 0.01f){
            animator.SetTrigger("fadeout");     //fades out scene
            SceneManager.LoadScene ("GameOver");
            
           // bool AnimatorIsPlaying(){
           // return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
          //  }


/*
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("fade_out"))
            {
                Debug.Log("Hi");
                animation_started = true;
            }
            else{
                // SceneManager.LoadScene ("GameOver");
                if (animation_started = true){
                     Debug.Log("Hello");
                }
               
            }
            */
        }
    }
}
