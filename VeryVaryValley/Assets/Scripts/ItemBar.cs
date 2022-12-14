using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBar : MonoBehaviour
{
    public static List<ItemSlot> itemSlots;
    public static GameObject selecting;
    public static int selectIndex;

    public static Item emptyItem;

    public TextMeshProUGUI itemText;

    public static ItemBar instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        itemSlots = new List<ItemSlot>();

        for(int i = 0; i < 10; i++)
        {
            itemSlots.Add(this.transform.GetChild(i).transform.GetChild(0).GetComponent<ItemSlot>());
        }

        emptyItem = (Item)Resources.Load("Items/Empty");

        selecting = transform.GetChild(11).gameObject;

        foreach(ItemSlot itemSlot in itemSlots)
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

    public static bool CheckExistence(Item newItem)
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

    public static int GetSlotIndex(Item newItem)
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

    public static ItemSlot GetFirstEmptySlot()
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
        //itemText.rectTransform.position = itemSlots[selectIndex].transform.parent.position;
        itemText.rectTransform.anchoredPosition = new Vector3(180.0f, -((selectIndex) * 100f) - 50f, 0);
        itemText.text = itemSlots[selectIndex].item.itemName;
    }
}