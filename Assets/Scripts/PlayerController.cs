using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float gravity;
    public Text countText;
    public Text winText;
    public Gun gun;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private int count;

    void Start () {
        controller = GetComponent<CharacterController>();
        gameObject.transform.position = new Vector3(0, 1, 0);  // Set initial position

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";
    }

    /*void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 1.0f, moveVertical);

        rb.AddForce (movement * speed);
    }*/

    void Update() {
        if (controller.isGrounded) {
            // Recalculate and move directly on axes
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown("Shoot")) {
            gun.Shoot();
        } else if (Input.GetButton("Shoot")) {
            gun.ShootContinuous();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ( "Pick Up")) {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
    }

    void SetCountText () {
        countText.text = "Score: " + count.ToString ();
        if (count >= 12) {
            winText.text = "You Win!";
        }
    }
}
