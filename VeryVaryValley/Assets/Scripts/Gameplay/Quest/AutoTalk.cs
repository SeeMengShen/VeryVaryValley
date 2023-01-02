using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoTalk : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 1 time auto talk to NPC when enters trigger
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.LookAt(transform.parent);
            transform.parent.GetComponent<NPC>().Talk();
            Destroy(gameObject);
        }
    }
}
