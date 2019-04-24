using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterController : MonoBehaviour
{
	void OnCollisionEnter(Collision other) {
		SceneManager.LoadScene("WinningScene");
	}
}
