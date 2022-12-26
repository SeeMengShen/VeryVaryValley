using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public Text quantityText;

    public int quantity = 0;
    private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSlotContent(Item newItem)
    {
        if(item != newItem)
        {
            item = newItem;

            icon.enabled = true;
            icon.sprite = newItem.icon;

            this.transform.parent.parent.GetComponent<ItemBar>().UpdateItemText();          
        }

        quantity++;
        quantityText.text = quantity.ToString();
    }

    public void MinusSlotContent()
    {
        quantity--;

        if (quantity > 0)
        {
            quantityText.text = quantity.ToString();
        }
        else
        {
            item = ItemBar.Instance.emptyItem;

            icon.enabled = false;
            icon.sprite = item.icon;

            this.transform.parent.parent.GetComponent<ItemBar>().UpdateItemText();

            quantityText.text = string.Empty;
        }
    }
}
