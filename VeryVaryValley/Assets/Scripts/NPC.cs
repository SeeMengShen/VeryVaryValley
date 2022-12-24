using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NPC : MonoBehaviour
{
    public TextAsset[] dialogue;
    public Quest[] quest;
    public int currentQuestIndex = 0;
    private SphereCollider sightArea;
    private string doneStr = "You have done the quest!";

    // Start is called before the first frame update
    void Start()
    {
        sightArea = GetComponent<SphereCollider>();
        sightArea.radius = 10.0f;
        sightArea.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Talk()
    {
        if(quest.Length > currentQuestIndex)
        {
            GameController.Instance.questPanel.alpha = 0.0f;
            GameController.Instance.questPanel.blocksRaycasts = false;
            GameController.Instance.InitQuest(quest[currentQuestIndex]);

            if (quest[currentQuestIndex].done)
            {
                if (currentQuestIndex < dialogue.Length - 1)
                {
                    currentQuestIndex++;
                }
            }
        }       

        GameController.Instance.AssignDialogue(dialogue[currentQuestIndex]);
        GameController.Instance.dialogueController.ResetAndStart();
        GameController.Instance.ActivateDialogue(true);

        GameController.Instance.fpc.m_MouseLook.SetCursorLock(false);

        
    }

    /*void OnTriggerStay(Collider other)
    {
        gameObject.transform.LookAt(other.gameObject.transform);
    }*/
}
