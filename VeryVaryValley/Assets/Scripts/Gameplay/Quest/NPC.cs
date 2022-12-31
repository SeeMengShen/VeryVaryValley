using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NPC : MonoBehaviour
{
    public TextAsset[] dialogue;
    public Quest[] quest;
    public int currentQuestIndex = 0;
    public GameObject questIndicator;

    public GameObject reward;

    private PlayerSight player;

    private bool gaveReward = false;
    private SphereCollider sightArea;

    private string crowNPC = "Crow NPC";

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

            LevelController.Instance.sideQuestPanel.alpha = 0.0f;
            LevelController.Instance.sideQuestPanel.blocksRaycasts = false;

            if (quest.Length > currentQuestIndex)
            {
                LevelController.Instance.InitSideQuest(quest[currentQuestIndex]);
            }
            else
            {
                if (!gaveReward)
                {
                    GiveReward();
                    gaveReward = true;
                    Destroy(questIndicator);
                }
            }
        }

        LevelController.Instance.AssignDialogue(dialogue[currentQuestIndex]);
        LevelController.Instance.dialogueController.ResetAndStart();
        LevelController.Instance.ActivateDialogue(true);
        LevelController.Instance.StopControl(true);
    }

    private void GiveReward()
    {
        player = LevelController.Instance.currentActiveCamera.GetComponent<PlayerSight>();
        player.Collect(reward);

        if (gameObject.name == crowNPC)
        {
            Item crowItem = (Item)Resources.Load("Items/Crow");
            ItemBar.Instance.itemSlots[ItemBar.Instance.GetSlotIndex(crowItem)].MinusSlotContent();
            ItemBar.Instance.itemSlots[ItemBar.Instance.GetSlotIndex(crowItem)].MinusSlotContent();
        }
    }

    // Look at player while player enters trigger (area of sight)
    void OnTriggerStay(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position ;
        direction.Set(direction.x, 0.0f, direction.z);
        
        // x axis rotation will happen in this condition, not sure why but return when it happens
        if((-0.01f <= direction.x && direction.x <= 0.01f) &&
            (-0.01f <= direction.y && direction.y <= 0.01f))
        {
            return;
        }

        Quaternion toRotation = Quaternion.FromToRotation(Vector3.forward, direction);        
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.001f * Time.time);
    }
}
