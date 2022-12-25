using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NPC : MonoBehaviour
{
    public TextAsset[] dialogue;
    public Quest[] quest;
    public int currentQuestIndex = 0;

    public GameObject reward;

    private bool gaveReward = false;
    private SphereCollider sightArea;

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
        if (quest.Length > currentQuestIndex && quest.Length != 0)
        {
            if (quest[currentQuestIndex].done)
            {
                if (currentQuestIndex < dialogue.Length - 1)
                {
                    currentQuestIndex++;
                }
            }

            GameController.Instance.questPanel.alpha = 0.0f;
            GameController.Instance.questPanel.blocksRaycasts = false;

            if (quest.Length > currentQuestIndex)
            {                
                GameController.Instance.InitQuest(quest[currentQuestIndex]);
            }
            else
            {
                if(!gaveReward)
                {
                    GiveReward();
                    gaveReward = true;
                }                
            }
        }       

        GameController.Instance.AssignDialogue(dialogue[currentQuestIndex]);
        GameController.Instance.dialogueController.ResetAndStart();
        GameController.Instance.ActivateDialogue(true);

        GameController.Instance.fpc.m_MouseLook.SetCursorLock(false);        
    }

    private void GiveReward()
    {
        reward.SetActive(true);
    }

    /*void OnTriggerStay(Collider other)
    {
        gameObject.transform.LookAt(other.gameObject.transform);
    }*/
}
