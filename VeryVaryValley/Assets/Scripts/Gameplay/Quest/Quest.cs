using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool mainQuest;
    public string questTitle;
    public string questDesc;
    public string questProgressStr;
    public int questProgress;
    public int questGoal;
    public bool done;
    private string slash = "/";

    // Start is called before the first frame update
    void Start()
    {
        questProgressStr = questProgress.ToString() + slash + questGoal.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuestProgress(int qty)
    {
        questProgress += qty;

        CheckQuestDone();

        questProgressStr = questProgress.ToString() + slash + questGoal.ToString();

        if(mainQuest)
        {
            LevelController.Instance.UpdateMainQuestProgressUI(questProgressStr);
        }
        else
        {
            LevelController.Instance.UpdateSideQuestProgressUI(questProgressStr);
        }        
    }

    public void CheckQuestDone()
    {
        if(questProgress < questGoal)
        {
            return;
        }

        done = true;

        if(mainQuest)
        {
            LevelController.Instance.UpdateMainQuestStatus(true);
        }
        else
        {
            LevelController.Instance.UpdateSideQuestStatus(true);
        }
        
    }
}
