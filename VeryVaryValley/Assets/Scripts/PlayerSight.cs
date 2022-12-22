using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSight : MonoBehaviour
{
    public RawImage crosshair;

    public GameObject head;

    public Using player;

    private RaycastHit hit;
    private GameObject interactable;
    private Item newItem;
    private ItemSlot itemSlot;

    private string textToShow;
    private const string itemResourceStr = "Items/";
    private const string collectText = "E to collect";
    private const string talkText = "E to talk";
    private const string interactText = "E to interact";
    private const string itemStr = "Item";
    private const string npcStr = "NPC";
    private const string interactableStr = "Interactable";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(GameController.Instance.stopControl)
        {
            return;
        }

        if (Physics.Raycast(head.transform.position, transform.forward, out hit, 3.0f, 3))
        {
            interactable = hit.collider.gameObject;

            CheckInteractable(interactable.tag);

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (interactable.tag)
                {
                    case itemStr:
                        Collect();
                        hit.collider.gameObject.GetComponent<Collectable>().Collected();
                        break;
                    case npcStr:
                        interactable.GetComponent<NPC>().Talk();
                        break;
                    case interactableStr:
                        interactable.GetComponent<Interactable>().Open();
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            textToShow = string.Empty;
            GameController.Instance.ShowHintText(textToShow);
            crosshair.color = Color.cyan;
        }

        /*Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, rayMask);
        Ray ray = c.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);*/
    }

    private void CheckInteractable(string objectTag)
    {
        if (objectTag == itemStr)
        {
            textToShow = collectText;
        }
        else if(objectTag == npcStr)
        {
             textToShow = talkText;
        }
        else if(objectTag == interactableStr)
        {
            textToShow = interactText;
        }
        else
        {
            textToShow = string.Empty;
        }

        GameController.Instance.ShowHintText(textToShow);
        crosshair.color = Color.magenta;
    }

    private void Collect()
    {
        newItem = (Item)Resources.Load(itemResourceStr + hit.collider.gameObject.name);

        if (!ItemBar.Instance.CheckExistence(newItem))
        {
            itemSlot = ItemBar.Instance.GetFirstEmptySlot();
        }
        else
        {
            itemSlot = ItemBar.Instance.itemSlots[ItemBar.Instance.GetSlotIndex(newItem)];
        }

        itemSlot.AddSlotContent(newItem);
    }

    public void Collect(GameObject toCollect)
    {
        newItem = (Item)Resources.Load(itemResourceStr + toCollect.name);

        if (!ItemBar.Instance.CheckExistence(newItem))
        {
            itemSlot = ItemBar.Instance.GetFirstEmptySlot();
        }
        else
        {
            itemSlot = ItemBar.Instance.itemSlots[ItemBar.Instance.GetSlotIndex(newItem)];
        }

        itemSlot.AddSlotContent(newItem);
    }
}