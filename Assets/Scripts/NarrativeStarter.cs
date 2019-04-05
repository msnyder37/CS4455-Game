using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrativeStarter : MonoBehaviour
{
    public void StartNarrative(){
        SceneManager.LoadScene ("Narrative");
    }
}
