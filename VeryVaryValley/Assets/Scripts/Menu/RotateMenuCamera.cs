using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuCamera : MonoBehaviour
{

    public float speed = 3.0f;
    public Transform targetCamera;
    public Transform followingObject;


    // Update is called once per frame
    void Update()
    {
        //camera look at rotating empty object
        targetCamera.transform.LookAt(followingObject.position);

        //the rotating empty is rotating, camera is the center
        followingObject.transform.RotateAround(targetCamera.position, Vector3.up, speed * Time.deltaTime);
    }
}
