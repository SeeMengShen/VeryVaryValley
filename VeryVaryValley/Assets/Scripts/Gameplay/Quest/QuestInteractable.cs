using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractable : MonoBehaviour
{
    public Quest quest;
    public Item questItem;
    private string errorText = "This is not where you should put it!";
    private string untaggedStr = "Untagged";
    private string potStr = "Pot";
    private int putIndex = 0;

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
        if(ItemBar.Instance.GetHoldingItem() == questItem)
        {
            ItemBar.Instance.GetSelectingItemSlot().MinusSlotContent();
            
            if(gameObject.name == potStr)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(putIndex).gameObject.SetActive(true);
                putIndex++;
            }
            
            quest.UpdateQuestProgress(1);

            if(quest.done)
            {
                gameObject.tag = untaggedStr;
            }
        }
        else
        {
            LevelController.Instance.ShowWarningText(errorText);
        }
        
    }
}
