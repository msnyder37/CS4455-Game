using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float jumpCooldown;
    public float gravity;
    public Transform spawn;
    public GunController gun;
    public bool hasKey1;
    public bool hasKey2;
    public bool hasKey3;

    private Rigidbody rb;
    private Camera mainCamera;
    private float distToGround;
    private float jumpClock;
    private Vector3 moveDir;

    void Start () {
        gameObject.transform.position = spawn.position;  // Set initial position
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        hasKey1 = false;
        hasKey2 = false;
        hasKey3 = false;
    }

    void FixedUpdate() {
        ControlMouse();
    }

    void Update() {
        if (Input.GetButtonDown("Shoot")) {
            gun.isFiring = true;
            gun.Shoot();
        } else if (Input.GetButton("Shoot")) {
            gun.isFiring = true;
            gun.ShootContinuous();
        } else if (Input.GetButtonUp("Shoot")) {
            gun.isFiring = false;
        }
    }

    void ControlMouse() {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength)) {
            Vector3 pointer = cameraRay.GetPoint(rayLength);
            //Debug.DrawLine(cameraRay.origin, pointer, Color.blue);
            transform.LookAt(new Vector3(pointer.x, transform.position.y, pointer.z));
        }

        // Recalculate and move directly on axes
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
        moveDir = moveDir * speed;
        if (IsGrounded()) {
            jumpClock -= Time.deltaTime;
            if (jumpClock <= 0) {
                jumpClock = jumpCooldown;
                if (Input.GetButton("Jump")) {
                    // Using impulse disables the ability to short hop
                    rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
                }
            }
        }

        // Apply gravity
        rb.AddForce(-transform.up * gravity, ForceMode.Acceleration);

        // Move the player
        rb.MovePosition(transform.position + moveDir * Time.deltaTime);
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public Vector3 GetMoveDir() {
        return moveDir;
    }

    void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case "Pick Up":
                other.gameObject.SetActive(false);
                break;
            case "Fixed Camera":
                mainCamera.GetComponent<CameraController>().fixedCamera = other.gameObject;
                mainCamera.GetComponent<CameraController>().fixedCameraBool = true;
                break;
            case "Kill Plane":
                this.transform.position = spawn.transform.position;
                break;
            case "Moving Platform":
                transform.SetParent(other.gameObject.transform);
                break;
            case "Key 1":
                other.gameObject.SetActive(false);
                hasKey1 = true;
                break;
            case "Key 2":
                other.gameObject.SetActive(false);
                hasKey2 = true;
                break;
            case "Key 3":
                other.gameObject.SetActive(false);
                hasKey3 = true;
                break;
        }
    }
    void OnTriggerExit(Collider other) {
        switch (other.gameObject.tag) {
            case "Fixed Camera":
                mainCamera.GetComponent<CameraController>().fixedCamera = other.gameObject;
                mainCamera.GetComponent<CameraController>().fixedCameraBool = false;
                mainCamera.GetComponent<CameraController>().exitingFixedCamera =  true;
                break;
            case "Moving Platform":
                transform.SetParent(null);
                break;
        }
    }
}
