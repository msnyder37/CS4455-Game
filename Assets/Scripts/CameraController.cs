using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public GameObject fixedCamera;
    public bool fixedCameraBool, exitingFixedCamera;
    public float cameraSpeed =.05f;


    void Start() {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() {
        if(fixedCameraBool) {
            Vector3 newPosition = new Vector3(fixedCamera.transform.position.x, transform.position.y, fixedCamera.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, cameraSpeed);
            // transform.position = player.transform.position + offset;
        }
    }




}
