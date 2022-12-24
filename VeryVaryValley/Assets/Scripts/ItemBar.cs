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
    }

    void Start()
    {
        /*itemSlots = new List<ItemSlot>();

        for(int i = 0; i < 10; i++)
        {
            itemSlots.Add(this.transform.GetChild(i).transform.GetChild(0).GetComponent<ItemSlot>());
        }

        emptyItem = (Item)Resources.Load("Items/Empty");

        selecting = transform.GetChild(11).gameObject;*/

        foreach (ItemSlot itemSlot in itemSlots)
        {
            itemSlot.item = emptyItem;
        }

        UpdateItemText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") == 0.0f)
        {
            return;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            selectIndex--;
        }
        else
        {
            selectIndex++;
        }

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