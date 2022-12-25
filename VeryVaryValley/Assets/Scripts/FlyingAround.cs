using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAround : MonoBehaviour
{
    private float timeCounter = 0;
    public float speed;
    public float width;
    public float height;
    public float rotationSpeed;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private float x;
    private float y;
    private float z;

    Vector3 movementDirection = new Vector3();

    void Start()
    {
        offsetX = transform.localPosition.x;
        offsetY = transform.localPosition.y;
        offsetZ = transform.localPosition.z;
    }

    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        x = Mathf.Cos(timeCounter) * width + offsetX;
        y = 45.0f;
        z = Mathf.Sin(timeCounter) * height + offsetZ;

        movementDirection.Set(x, y, z);

        transform.localPosition = movementDirection;

        movementDirection.Set(0.0f, 0.0f, z);
        transform.forward = movementDirection;

        /*Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);*/
    }
}
