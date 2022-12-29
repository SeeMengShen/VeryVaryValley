using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    public int defaultQty;
    public int availableQty;

    public bool unlimited;

    // Start is called before the first frame update
    void Start() {
        availableQty = defaultQty;
    }

    // Update is called once per frame
    void Update() {

    }

    public void Collected() {
        if (availableQty > 1) {
            availableQty--;
        }

        else {
            if (unlimited) {
                LevelController.Instance.ResetCollectable(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }
    }

    //to play drop sound
    public void OnCollisionEnter(Collision collision) {
        if (collision.collider.name != "Hanging") {
            Debug.Log(gameObject.name + " " + collision.collider.name);
            AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.dropEffect);
        }
    }
}
