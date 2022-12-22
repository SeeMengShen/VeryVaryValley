using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBushesRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string BushName = "Bush";
        GameObject[] bushes = GameObject.FindGameObjectsWithTag(BushName);
        float angle;
        Vector3 centerPoint;

        foreach(GameObject bush in bushes) {
            angle = Random.Range(0.0f, 360.0f);

            centerPoint = bush.GetComponent<Renderer>().bounds.center;
            //bush.transform.Rotate(0.0f, angle,0.0f);
            bush.transform.RotateAround(centerPoint, Vector3.forward, angle);
        }

    }

}
