using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //  Auto cleanup to save memory
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
