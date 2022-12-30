using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSight : MonoBehaviour
{
    public RawImage crosshair;

    public GameObject head;

    private RaycastHit hit;
    private GameObject interactable;
    private Item newItem;
    private ItemSlot itemSlot;

    private string textToShow;
    private const string ITEM_RESOURCE_STR = "Items/";
    private const string COLLECT_TEXT = "E to collect";
    private const string TALK_TEXT = "E to talk";
    private const string INTERACT_TEXT = "E to interact";
    private const string PUT_TEXT = "E to put";
    private const string ITEM_STR = "Item";
    private const string NPC_STR = "NPC";
    private const string QUEST_STR = "Quest";
    private const string INTERACTABLE_STR = "Interactable";

    private const string GRABGUN_STR = "GrabGun";

    // Start is called before the first frame update
    void Start()
    {
        Item grabGunItem = (Item)Resources.Load(ITEM_RESOURCE_STR + GRABGUN_STR);
        grabGunItem.usable = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(LevelController.Instance.GetStopControl())
        {
            return;
        }
                
        if (Physics.Raycast(head.transform.position, transform.forward, out hit, 3.0f, 3, QueryTriggerInteraction.Ignore))
        {
            interactable = hit.collider.gameObject;

            CheckInteractable(interactable.tag);

            if (Input.GetKeyUp(KeyCode.E))
            {
                switch (interactable.tag)
                {
                    case ITEM_STR:
                        Collect();
                        hit.collider.gameObject.GetComponent<Collectable>().Collected();
                        break;
                    case NPC_STR:
                        interactable.GetComponent<NPC>().Talk();
                        break;
                    case INTERACTABLE_STR:
                        interactable.GetComponent<Interactable>().Open();
                        break;
                    case QUEST_STR:
                        if(ItemBar.Instance.IsHoldingQuestItem())
                        {
                            interactable.GetComponent<QuestInteractable>().Put();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        else
        {
            textToShow = string.Empty;
            LevelController.Instance.ShowHintText(textToShow);
            crosshair.color = Color.cyan;
        }
    }

    private void CheckInteractable(string objectTag)
    {

        switch(objectTag)
        {
            case ITEM_STR:
                textToShow = COLLECT_TEXT;
                break;
            case NPC_STR:
                textToShow = TALK_TEXT;
                break;
            case INTERACTABLE_STR:
                textToShow = INTERACT_TEXT;
                break;
            case QUEST_STR:
                if(ItemBar.Instance.IsHoldingQuestItem())
                {
                    textToShow = PUT_TEXT;
                }
                break;
            default:
                textToShow = string.Empty;
                return;
        }

        LevelController.Instance.ShowHintText(textToShow);
        crosshair.color = Color.magenta;
    }

    private void Collect()
    {
        newItem = (Item)Resources.Load(ITEM_RESOURCE_STR + hit.collider.gameObject.name);

        if (!ItemBar.Instance.CheckExistence(newItem))
        {
            itemSlot = ItemBar.Instance.GetFirstEmptySlot();
        }
        else
        {
            itemSlot = ItemBar.Instance.itemSlots[ItemBar.Instance.GetSlotIndex(newItem)];
        }

        itemSlot.AddSlotContent(newItem);

        if(!newItem.throwable && !newItem.usable)                       // Not throwable and not usable (Quest Item)
        {
            if (newItem.name == GRABGUN_STR)
            {
                newItem.usable = true;
            }
            hit.collider.gameObject.GetComponent<QuestItem>().Take();
        }


        //play pick up sound
        AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.pickUpEffect);
    }

    //grab gun only
    public void Collect(GameObject toCollect)
    {
        newItem = (Item)Resources.Load(ITEM_RESOURCE_STR + toCollect.name);

        if (!ItemBar.Instance.CheckExistence(newItem))
        {
            itemSlot = ItemBar.Instance.GetFirstEmptySlot();
        }
        else
        {
            itemSlot = ItemBar.Instance.itemSlots[ItemBar.Instance.GetSlotIndex(newItem)];
        }

        itemSlot.AddSlotContent(newItem);

        if (!newItem.throwable && !newItem.usable)                       // Not throwable and not usable (Quest Item)
        {
            toCollect.gameObject.GetComponent<QuestItem>().Take();
        }
    }
}