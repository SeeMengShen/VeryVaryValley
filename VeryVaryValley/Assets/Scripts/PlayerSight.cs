using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSight : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public RawImage crosshair;

    public GameObject head;

    private RaycastHit hit;
    private GameObject interactable;
    private Item newItem;
    private ItemSlot itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Physics.Raycast(head.transform.position, transform.forward, out hit, 3.0f, 3))
        {
            interactable = hit.collider.gameObject;

            CheckInteractable(interactable.tag);

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (interactable.tag)
                {
                    case "Item":
                        Collect();
                        break;
                    case "NPC":
                        interactable.GetComponent<NPC>().Talk();
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            crosshair.color = Color.cyan;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 3f;
        Debug.DrawRay(head.transform.position, transform.forward, Color.green);

        /*Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, rayMask);
        Ray ray = c.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);*/
    }

    private void CheckInteractable(string objectTag)
    {
        if (objectTag == "Item")
        {
            tmpro.text = "E to collect";
        }
        else
        {
            tmpro.text = "E to talk";
        }

        crosshair.color = Color.magenta;
    }

    private void Collect()
    {
        newItem = (Item)Resources.Load("Items/" + hit.collider.gameObject.name);

        if (!ItemBar.CheckExistence(newItem))
        {
            itemSlot = ItemBar.GetFirstEmptySlot();
        }
        else
        {
            itemSlot = ItemBar.itemSlots[ItemBar.GetSlotIndex(newItem)];
        }

        itemSlot.AddSlotContent(newItem);
    }
}
