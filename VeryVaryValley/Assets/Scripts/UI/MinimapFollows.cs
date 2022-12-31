using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollows : MonoBehaviour
{
    public GameObject player;
    private Vector3 positionHolder = new Vector3();

    void Update()
    {
        positionHolder.Set(player.transform.position.x, 800.0f, player.transform.position.z);
        transform.position = positionHolder;
    }
}
