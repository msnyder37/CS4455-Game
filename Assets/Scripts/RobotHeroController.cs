using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeroController : MonoBehaviour
{
    public GameObject rifle;
    public Transform rifleBone;

    private Animator animator;
    private bool carryRifle;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.carryRifle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // alternate carrying rifle
            if (this.carryRifle)
            {
                // put rifle away
                Destroy(this.rifleBone.GetChild(0).gameObject);
            }
            else
            {
                // get rifle out
                GameObject newRifle = Instantiate<GameObject>(this.rifle);
                newRifle.transform.parent = this.rifleBone;
                newRifle.transform.localPosition = Vector3.zero;
                newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
            }

            this.carryRifle = !this.carryRifle;
            this.animator.SetBool("CarryRifle", this.carryRifle);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            // fire the gun or punch
            this.animator.SetTrigger("Action");
        }

        float forward = Input.GetAxisRaw("Vertical");
        float turn = Input.GetAxisRaw("Horizontal");

        float angleInput = Mathf.Atan2(turn, forward);
        float angleRobot = this.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float inputMagnitude = Vector3.Magnitude(new Vector3(forward, turn, 0));

        // perform input projection
        forward = inputMagnitude * Mathf.Cos(angleRobot - angleInput);
        turn = -1.0f * inputMagnitude * Mathf.Sin(angleRobot - angleInput);

        this.animator.SetFloat("Forward", forward);
        this.animator.SetFloat("Turn", turn);
    }
}
