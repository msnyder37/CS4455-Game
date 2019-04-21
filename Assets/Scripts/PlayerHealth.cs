using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health_player = 1.00f;    //health is ranges between 0.00 to 1.00
    public Slider healthbar;  

    
    void Update()
    {
        //The slider value always gets the value of the public float health_player
        healthbar.value = health_player;

        //If healthbar drops too low, load the game over screen
        if (health_player < 0.01f){
            SceneManager.LoadScene ("GameOver");
        }
    }
}
