using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NPC : MonoBehaviour
{
    public TextAsset dialogue;
    public Quest quest;
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
        if (quest != null && quest.done)
        {
            GameController.Instance.ShowFiveSecondText(doneStr);
            return;
        }

        GameController.Instance.questPanel.alpha = 0.0f;
        GameController.Instance.questPanel.blocksRaycasts = false;
        GameController.Instance.InitQuest(quest);

        GameController.Instance.AssignDialogue(dialogue);
        GameController.Instance.dialogueController.ResetAndStart();
        GameController.Instance.ActivateDialogue(true);

        GameController.Instance.fpc.m_MouseLook.SetCursorLock(false);
    }

    /*void OnTriggerStay(Collider other)
    {
        gameObject.transform.LookAt(other.gameObject.transform);
    }*/
}
