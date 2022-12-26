using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuCamera : MonoBehaviour {

    public static RotateMenuCamera Instance = null;

    public float speed = 3.0f;
    public Transform targetCamera;
    public Transform followingObject;    

    private static bool hasRandomizedCameraPosition = false;    

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        if (!hasRandomizedCameraPosition) {
            followingObject.transform.RotateAround(targetCamera.position, Vector3.up, Random.Range(1, 361));
            GameManager.Instance.followingObject = followingObject.transform;
            hasRandomizedCameraPosition =true;
        }
        else {
            //followingObject = GameManager.Instance.followingObject;
        }
    }

    // Update is called once per frame
    void Update() {
        GameManager.Instance.followingObject = followingObject.transform;

        //camera look at rotating empty object
        targetCamera.transform.LookAt(
            GameManager.Instance.followingObject.position
            );

        //the rotating empty is rotating, camera is the center
        followingObject.transform.RotateAround(targetCamera.position, Vector3.up, speed * Time.deltaTime);        
    }
}
