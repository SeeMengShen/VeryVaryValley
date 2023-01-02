using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int defaultQty;
    public int availableQty;

    public bool unlimited;

    private const string HANGING_STR = "Hanging";

    // Start is called before the first frame update
    void Start()
    {
        availableQty = defaultQty;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //  Invoked when a collectable item in the world is being collected
    public void Collected()
    {
        if (availableQty > 1)
        {
            availableQty--;
        }

        else
        {
            if (unlimited)  // Cooldown to refill when ran out of quantity
            {
                LevelController.Instance.ResetCollectable(gameObject);
            }
            else            // Destroy when it is not unlimited
            {
                Destroy(gameObject);
            }
        }
    }

    //to play drop sound
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != HANGING_STR)
        {
            AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.dropEffect);
        }
    }
}
