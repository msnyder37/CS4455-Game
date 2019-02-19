using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeroController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float turn = Input.GetAxisRaw("Horizontal");

        this.animator.SetFloat("Forward", forward);
        this.animator.SetFloat("Turn", turn);
    }
}
