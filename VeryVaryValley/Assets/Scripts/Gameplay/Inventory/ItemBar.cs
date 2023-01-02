using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBar : MonoBehaviour
{
    public static ItemBar Instance = null;

    public List<ItemSlot> itemSlots;
    public GameObject selecting;
    public int selectIndex = 0;

    public Item emptyItem;

    public TextMeshProUGUI itemText;

    private Vector3 textPosition = new Vector3(180.0f, -50f, 0.0f);

    private const string MOUSE_SCR_WHL = "Mouse ScrollWheel";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Initialize all item slots with empty object first
        foreach (ItemSlot itemSlot in itemSlots)
        {
            itemSlot.item = emptyItem;
        }

        UpdateItemText();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelController.Instance.GetStopControl())
        {
            return;
        }

        
        if(Input.GetAxis(MOUSE_SCR_WHL) == 0.0f)
        {
            return;
        }

        // When scroll wheel is detected, change the index value
        if (Input.GetAxis(MOUSE_SCR_WHL) > 0.0f)
        {
            selectIndex--;
        }
        else
        {
            selectIndex++;
        }

        // Loop back to first one if exceeded max
        if(selectIndex > itemSlots.Count - 1)
        {
            selectIndex = 0;
        }
        else if(selectIndex < 0)
        {
            selectIndex = itemSlots.Count - 1;
        }

        selecting.transform.position = itemSlots[selectIndex].transform.parent.position;

        UpdateItemText();
    }

    // Check existence of an object in the inventory to prevent duplication
    public bool CheckExistence(Item newItem)
    {
        foreach(ItemSlot itemSlot in itemSlots)
        {
            if(itemSlot.item == newItem)
            {
                return true;
            }
        }

        return false;
    }

    public int GetSlotIndex(Item newItem)
    {
        for(int i = 0; i < itemSlots.Count - 1; i++)
        {
            if(itemSlots[i].item == newItem)
            {
                return i;
            }
        }

        return -1;
    }

    public ItemSlot GetFirstEmptySlot()
    {
        for(int i = 0; i < itemSlots.Count - 1; i++)
        {
            if(itemSlots[i].item == emptyItem)
            {
                return itemSlots[i];
            }
        }

        return null;
    }

    public void UpdateItemText()
    {
        textPosition.Set(180.0f, -((selectIndex) * 100f) - 50f, 0);
        itemText.rectTransform.anchoredPosition = textPosition;
        itemText.text = itemSlots[selectIndex].item.itemName;
    }

    public ItemSlot GetSelectingItemSlot()
    {
        return itemSlots[selectIndex];
    }

    public Item GetHoldingItem()
    {
        return itemSlots[selectIndex].item;
    }

    public bool IsHoldingEmpty()
    {
        if (itemSlots[selectIndex].item == emptyItem)
        {
            return true;
        }

        return false;
    }

    public bool IsHoldingThrowable()
    {
        return GetHoldingItem().throwable;
    }

    public bool IsHoldingUsable()
    {
        return GetHoldingItem().usable;
    }

    public bool IsHoldingQuestItem()
    {
        return !IsHoldingThrowable() && !IsHoldingUsable() && !IsHoldingEmpty();
    }

}