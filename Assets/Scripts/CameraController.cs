using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public GameObject fixedCamera;
    public bool fixedCameraBool, exitingFixedCamera;
    public float cameraSpeed = .05f;
    public float lookAhead;
    public bool useLookAhead = false;
    public float zOffset;


    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (fixedCameraBool)
        {
            Vector3 newPosition = new Vector3(fixedCamera.transform.position.x, fixedCamera.transform.position.y, fixedCamera.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed);
        }
        else
        {
            if (useLookAhead)
            {
                Vector3 moveDir = player.GetComponent<RobotHeroController>().GetMoveDir();
                moveDir = Vector3.Scale(moveDir, new Vector3(lookAhead, 0, lookAhead));
                Vector3 newPosition = new Vector3(player.transform.position.x + moveDir.x, player.transform.position.y, player.transform.position.z + moveDir.z);

                transform.position = Vector3.Lerp(transform.position, newPosition + offset, cameraSpeed);
            } else {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - zOffset) + offset, cameraSpeed);
            }
        }
    }
}
