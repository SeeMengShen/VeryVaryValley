using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTrash : MonoBehaviour {

    private int minTimeInterval = 0;
    public int maxTimeInterval = 599;

    public float speed = 0.15f;
    List<Trash> trashList = new List<Trash>();

    private class Trash {
        public GameObject trash = null;
        public int counter = 0;
        public bool isGoingUp;

        public Trash(GameObject trash, int counter, bool isGoingUp) {
            this.trash = trash;
            this.counter = counter;
            this.isGoingUp = isGoingUp;
        }
    }

    public static FloatingTrash Instance = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }





        string TrashName = "Trash";
        GameObject[] trashesTemp = GameObject.FindGameObjectsWithTag(TrashName);


        foreach (GameObject trashTemp in trashesTemp) {
            trashList.Add(
                new Trash(trashTemp, Random.Range(minTimeInterval, maxTimeInterval), RangeBoolean())
                );
        }
        
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        foreach (var trash in trashList) {
            if (trash.isGoingUp) {
                trash.trash.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            }
            else {
                trash.trash.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            }

            trash.counter++;

            if (trash.counter >= maxTimeInterval) {
                trash.counter = minTimeInterval;
                trash.isGoingUp = !trash.isGoingUp;
                
            }




        }
    }

    private bool RangeBoolean() {
        return (Random.value > 0.5f);
    }
}
