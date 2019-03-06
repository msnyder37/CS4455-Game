using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    public float spin_speed;

    // Start is called before the first frame update
    void Start()
    {
        spin_speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, spin_speed, 0);
            //Rotaional speed of object
    }
}
