using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public float timeLimit; //timelimit in seconds
    public Text timeLimitDisplay;  
    int min;
    int sec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        min = Mathf.FloorToInt(timeLimit / 60);
        sec = Mathf.FloorToInt(timeLimit % 60);
        if ( timeLimit < 0 )
        {
           SceneManager.LoadScene ("GameOver");
        }

        timeLimitDisplay.text = min.ToString("00") + ":" + sec.ToString("00");
    }

}