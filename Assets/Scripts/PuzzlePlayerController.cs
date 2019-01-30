using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls the movement of the puzzle player
/// 
/// Author: Joey Sterling
/// </summary>
public class PuzzlePlayerController : MonoBehaviour
{
    private GameObject[] cubes;
    private bool isMoving;

    private Vector3 startLocation;
    private Vector3 endLocation;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isMoving)
        {
            // delta defined to ensure constant time between points to ensure the illusion
            float delta = Vector3.Distance(this.startLocation, this.endLocation) * Time.deltaTime * this.speed;
            transform.position = Vector3.MoveTowards(transform.position, this.endLocation, delta);

            if (Vector3.Distance(transform.position, this.endLocation) < 0.001f)
            {
                // the position is close enough to teleport to the end location without being noticed
                transform.position = this.endLocation;
                this.isMoving = false;
            }
        }
        else
        {
            // update cube locations
            this.cubes = GameObject.FindGameObjectsWithTag("PuzzleCube");

            // process keyboard input to move the player
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ForwardAction(Vector3.forward);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                ForwardAction(Vector3.right);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                BackwardAction(Vector3.back);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                BackwardAction(Vector3.left);
            }
        }
    }

    /// <summary>
    /// Moves the player forward one spot on either of the forward axis (+x or +z)
    /// </summary>
    /// <param name="axis">The corresponding unit axis vector.</param>
    private void ForwardAction(Vector3 axis)
    {
        // check to see if there is ground in the forward direction on the axis
        if (ValidPlayerLocation(transform.position + axis))
        {
            // check for higher level (overlapping cube of higher y value)
            Vector3 desiredGround = transform.position + axis + Vector3.down;
            desiredGround = FindOverlappingCube(desiredGround, true);

            // translate forward one on the axis
            this.startLocation = transform.position;
            this.endLocation = desiredGround + Vector3.up;
            this.isMoving = true;
        }
    }

    /// <summary>
    /// Moves the player backwards one spot on either of the backwards axis (-x or -z)
    /// </summary>
    /// <param name="axis">The corresponding unit axis vector.</param>
    private void BackwardAction(Vector3 axis)
    {
        Vector3 desiredLocation = transform.position;

        if (ValidPlayerLocation(transform.position + axis))
        {
            // there is ground in the backwards direction on the axis
            desiredLocation = transform.position + axis;
        }
        else
        {
            // no ground directly behind on same elevation (y), maybe there's
            // a cube at a lower level that is being overlapped

            // check for lower level
            Vector3 desiredGround = transform.position + axis + Vector3.down;
            Vector3 lowerGround = FindOverlappingCube(desiredGround, false);

            if (lowerGround != desiredGround)
            {
                // there is a lower level (lower y) to translate to
                desiredLocation = lowerGround + Vector3.up;
            }
        }

        if (desiredLocation != transform.position)
        {
            // translate backwards one on the axis
            this.startLocation = transform.position;
            this.endLocation = desiredLocation;
            this.isMoving = true;
        }
    }

    /// <summary>
    /// Determines if the desired player location has "ground" underneath
    /// </summary>
    /// <returns><c>true</c>, if player location has "ground" underneath, <c>false</c> otherwise.</returns>
    /// <param name="desiredLocation">Desired location of the player object.</param>
    private bool ValidPlayerLocation(Vector3 desiredLocation)
    {
        // check for intersections/collisions
        foreach (GameObject cube in this.cubes)
        {
            if (desiredLocation == cube.transform.position)
            {
                return false;   // intersects a cube location
            }
        }

        // check for ground underneath
        Vector3 desiredGround = desiredLocation + Vector3.down;
        foreach (GameObject cube in this.cubes)
        {
            if (desiredGround == cube.transform.position)
            {
                return true;    // has no intersection and has a ground
            }
        }

        // no intersection (good), but no ground underneath
        return false;
    }

    /// <summary>
    /// Finds the overlapping cube for a specified ground cube.
    /// </summary>
    /// <returns>The overlapping cube, or the original ground cube.</returns>
    /// <param name="groundLocation">Ground cube location.</param>
    /// <param name="takeHigher">If set to <c>true</c> take higher y-value overlapping cube.</param>
    private Vector3 FindOverlappingCube(Vector3 groundLocation, bool takeHigher)
    {
        foreach (GameObject cube in this.cubes)
        {
            if (groundLocation != cube.transform.position)
            {
                // find coordinate differences to see if they follow overlapping rule
                float deltax = groundLocation.x - cube.transform.position.x;
                float deltay = cube.transform.position.y - groundLocation.y;
                float deltaz = groundLocation.z - cube.transform.position.z;

                if (deltax == deltay && deltay == deltaz)
                {
                    // this cube overlaps the groundLocation cube
                    if ((takeHigher && cube.transform.position.y > groundLocation.y)
                        || (!takeHigher && cube.transform.position.y < groundLocation.y))
                    {
                        return cube.transform.position;
                    }
                }
            }
        }

        return groundLocation;
    }
}
