using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging : MonoBehaviour
{
    private string stoneStr = "Stone(Clone)";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Only destroy itself when it is hit by stone
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == stoneStr)
        {
            Destroy(gameObject);
        }
    }
}
