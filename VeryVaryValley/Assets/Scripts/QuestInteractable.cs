using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractable : MonoBehaviour
{
    public Quest quest;
    public Item questItem;
    private string errorText = "This is not where you should put it!";
    private string untaggedStr = "Untagged";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Put()
    {
        if(ItemBar.Instance.itemSlots[ItemBar.Instance.selectIndex].item == questItem)
        {
            ItemBar.Instance.GetSelectingItemSlot().MinusSlotContent();
            gameObject.tag = untaggedStr;
            transform.GetChild(0).gameObject.SetActive(true);
            quest.UpdateQuestProgress(1);
        }
        else
        {
            GameController.Instance.ShowFiveSecondText(errorText);
        }
        
    }
}
