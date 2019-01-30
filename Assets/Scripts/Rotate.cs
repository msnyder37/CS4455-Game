using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the rotation of the cubes representing the ground of the puzzle
/// 
/// Author: Joey Sterling
/// </summary>
public class Rotate : MonoBehaviour
{
    private bool rotatingClockwise;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.rotatingClockwise = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerIsOnRotatingSection())
        {
            if (this.rotatingClockwise && (transform.eulerAngles.z > 270 || transform.eulerAngles.z <= 0))
            {
                transform.Rotate(new Vector3(0, 0, -1));
            }
            else if (!this.rotatingClockwise && transform.eulerAngles.z > 0)
            {
                transform.Rotate(new Vector3(0, 0, 1));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.rotatingClockwise = !this.rotatingClockwise;
            }
        }
    }

    private bool PlayerIsOnRotatingSection()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform cube = transform.GetChild(i);
            if (Vector3.Distance(this.player.transform.position, cube.position) <= Mathf.Sqrt(1.25f))
            {
                return true;
            }
        }
        return false;
    }
}
