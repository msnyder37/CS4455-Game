using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float gravity;
    public GunController gun;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Camera mainCamera;

    void Start () {
        controller = GetComponent<CharacterController>();
        gameObject.transform.position = new Vector3(0, 1, 0);  // Set initial position
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update() {
        ControlMouse();

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

        if (controller.isGrounded) {
            // Recalculate and move directly on world axes
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive (false);
        } else if (other.gameObject.CompareTag("Fixed Camera")) {
            // Debug.Log("Entered Camera Zone");
            mainCamera.GetComponent<CameraController>().fixedCamera = other.gameObject;
            mainCamera.GetComponent<CameraController>().fixedCameraBool = true;
        }
    }
    void OnTriggerExit(Collider other) {

        if (other.gameObject.CompareTag("Fixed Camera")) {
            // Debug.Log("Exited Camera Zone");
            mainCamera.GetComponent<CameraController>().fixedCamera = other.gameObject;
            mainCamera.GetComponent<CameraController>().fixedCameraBool = false;
            mainCamera.GetComponent<CameraController>().exitingFixedCamera =  true;
        }

    }
}
