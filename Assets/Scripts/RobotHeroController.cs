using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeroController : MonoBehaviour
{
    public GameObject rifle;
    public Transform rifleBone;
    public Transform hipBone;
    public float angleRate = 2.5f;
    public float jumpForce = 11.5f;
    public GameObject spawn;

    private Animator animator;
    private Camera mainCamera;
    private bool carryRifle;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;
    private GunController rifleController;

    public bool hasKey1;
    public bool hasKey2;
    public bool hasKey3;

    private float distanceToGround;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.mainCamera = FindObjectOfType<Camera>();
        this.carryRifle = false;
        this.capsuleCollider = this.GetComponent<CapsuleCollider>();
        this.rb = this.GetComponent<Rigidbody>();

        this.distanceToGround = this.hipBone.transform.position.y;
        this.rifleController = null;

        this.hasKey1 = false;
        this.hasKey2 = false;
        this.hasKey3 = false;
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
                this.rifleController = null;
            }
            else
            {
                // get rifle out
                GameObject newRifle = Instantiate<GameObject>(this.rifle);
                newRifle.transform.parent = this.rifleBone;
                newRifle.transform.localPosition = Vector3.zero;
                newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
                newRifle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                this.rifleController = newRifle.GetComponent<GunController>();
            }

            this.carryRifle = !this.carryRifle;
            this.animator.SetBool("CarryRifle", this.carryRifle);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            // fire the gun or punch
            this.animator.SetTrigger("Action");
        }

        if (Input.GetButtonDown("Jump") && this.IsGrounded())
        {
            // robot jumps
            this.rb.AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);
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

        // move towards desired angle
        float angleX = Input.GetAxisRaw("AngleX");
        float angleY = Input.GetAxisRaw("AngleY");

        if (angleX != 0.0f || angleY != 0.0f)
        {
            float desiredAngle = Mathf.Atan2(angleX, angleY) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, desiredAngle, this.transform.eulerAngles.z);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * this.angleRate);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pick Up":
                other.gameObject.SetActive(false);
                break;
            case "Fixed Camera":
                this.mainCamera.GetComponent<CameraController>().fixedCamera = other.gameObject;
                this.mainCamera.GetComponent<CameraController>().fixedCameraBool = true;
                break;
            case "Kill Plane":
                this.transform.position = spawn.transform.position;
                break;
            case "Moving Platform":
                this.transform.SetParent(other.gameObject.transform);
                break;
            case "Key 1":
                other.gameObject.SetActive(false);
                this.hasKey1 = true;
                break;
            case "Key 2":
                other.gameObject.SetActive(false);
                this.hasKey2 = true;
                break;
            case "Key 3":
                other.gameObject.SetActive(false);
                this.hasKey3 = true;
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fixed Camera":
                this.mainCamera.GetComponent<CameraController>().fixedCamera = other.gameObject;
                this.mainCamera.GetComponent<CameraController>().fixedCameraBool = false;
                this.mainCamera.GetComponent<CameraController>().exitingFixedCamera = true;
                break;
            case "Moving Platform":
                transform.SetParent(null);
                break;
        }
    }

    void FireRifle()
    {
        // triggered by animation event to fire the bullet at the right time
        this.rifleController.isFiring = true;
        this.rifleController.Shoot();
        this.rifleController.isFiring = false;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(this.hipBone.position, Vector3.down, this.distanceToGround + 0.1f);
    }
}
