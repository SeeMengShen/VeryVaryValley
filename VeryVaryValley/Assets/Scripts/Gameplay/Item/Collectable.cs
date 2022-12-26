using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int defaultQty;
    public int availableQty;

    public bool unlimited;

    // Start is called before the first frame update
    void Start()
    {
        availableQty = defaultQty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected()
    {        
        if(availableQty > 1)
        {
            availableQty--;
        }
        else
        {
            if (unlimited)
            {
                LevelController.Instance.ResetCollectable(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    
}
