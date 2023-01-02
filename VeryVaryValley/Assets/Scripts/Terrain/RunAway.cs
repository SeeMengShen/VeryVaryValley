using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
    private float previousDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When an object enters the trigger, run with the opposite direction of the moving object
    void OnTriggerStay(Collider other)
    {
        Vector3 distance = transform.position - other.transform.position;
        distance.y = 0.0f;
        float moveSpeed = 10.0f - distance.magnitude;

        // If the thing is approaching, run away
        if(distance.magnitude < previousDistance)
        {
            transform.localPosition += distance * moveSpeed * Time.deltaTime;

            distance.Set(distance.x, 0.0f, distance.z);
            Quaternion toRotation = Quaternion.FromToRotation(Vector3.forward, distance);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.001f * Time.time);
        }
        
        previousDistance = distance.magnitude;
    }
}
