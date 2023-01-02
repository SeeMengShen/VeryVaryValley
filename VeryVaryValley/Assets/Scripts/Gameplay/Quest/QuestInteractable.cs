using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractable : MonoBehaviour
{
    public Quest quest;
    public Item questItem;
    private const string ERROR_STR = "This is not where you should put it!";
    private const string UNTAGGED_STR = "Untagged";
    private const string POT_STR = "Pot";
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
            
            //  Special hard coded for acorn quest as a pot only takes 1 acorn
            if(gameObject.name == POT_STR)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                gameObject.tag = UNTAGGED_STR;
            }
            else
            {
                // Special hard coded for trash bin
                transform.GetChild(putIndex).gameObject.SetActive(true);
                putIndex++;
            }
            
            quest.UpdateQuestProgress(1);

            if(quest.done)
            {
                gameObject.tag = UNTAGGED_STR;
            }


            //play put acorn sound effect
            AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.placeEffect);


        }
        else
        {
            // Display warning message when wrong item is attempted
            LevelController.Instance.ShowWarningText(ERROR_STR);
        }
        
    }
}
