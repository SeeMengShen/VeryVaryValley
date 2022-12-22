using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrianScript : MonoBehaviour {

    public static TerrianScript Instance = null;


    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
