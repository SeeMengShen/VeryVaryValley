using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    public TextMeshProUGUI uiHintText;
    public TextMeshProUGUI uiWarningText;
    public TextMeshProUGUI grabText;
    public Image power;
    private const float maxPower = 1.0f;
    private const float minPower = 0.0f;
    public float currentPower = 0.0f;
    private float chargingSpeed = 0.01f;
    private bool increase = true;
    private Vector3 scaleHolder;
    private Color colorHolder;

    private bool showMap;

    public FirstPersonController fpc;

    public BasicInkExample dialogueController;
    public CanvasGroup dialoguePanel;

    public CanvasGroup map;

    public Quest currentQuest;
    public CanvasGroup questPanel;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDesc;
    public TextMeshProUGUI questProgressStr;
    public TextMeshProUGUI questStatus;
    public bool acceptQuest = true;

    public bool stopControl;

    private string doneStr = "Done";
    private string inProgStr = "In Progress";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scaleHolder = new Vector3(1.0f, 0.0f, 1.0f);
        colorHolder = new Color(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.M))
        {
            showMap = !showMap;
        }

        if(showMap)
        {
            map.alpha = 1.0f;
        }
        else
        {
            map.alpha = 0.0f;
        }

        map.blocksRaycasts = showMap;
        map.interactable = showMap;
    }

    public void ShowHintText(string text)
    {
        /*if (text == string.Empty)
          {
              //uiHintText.gameObject.SetActive(false);
              return;
          }*/

        //uiHintText.gameObject.SetActive(true);
        uiHintText.text = text;
    }

    public void ShowGrabText(string text)
    {
        grabText.text = text;
    }

    public void ShowWarningText(string text)
    {
        StartCoroutine(ShowFiveSecondText(text));
    }

    public IEnumerator ShowFiveSecondText(string text)
    {
        uiWarningText.text = text;
        yield return new WaitForSeconds(5.0f);
        uiWarningText.text = string.Empty;
    }

    public void ResetCollectable(GameObject collectable)
    {
        Instance.StartCoroutine(ResetC(collectable));
    }

    private IEnumerator ResetC(GameObject collectable)
    {
        collectable.GetComponent<Collectable>().availableQty = collectable.GetComponent<Collectable>().defaultQty;
        collectable.SetActive(false);
        yield return new WaitForSeconds(10f);
        collectable.SetActive(true);
    }

    public void PlayerCharging()
    {
        if (increase)
        {
            currentPower += chargingSpeed;

            if (currentPower >= maxPower)
            {
                increase = false;
                currentPower = maxPower;
            }
        }
        else
        {
            currentPower -= chargingSpeed;

            if (currentPower <= minPower)
            {
                increase = true;
                currentPower = minPower;
            }
        }

        UpdatePowerBar();
    }

    public void UpdatePowerBar()
    {
        scaleHolder.Set(1.0f, currentPower, 1.0f);
        colorHolder.r = currentPower;
        colorHolder.g = currentPower / 2.0f;

        power.rectTransform.localScale = scaleHolder;
        power.color = colorHolder;
    }

    public void ResetPowerBar()
    {
        currentPower = 0.0f;
        UpdatePowerBar();
    }

    public void AssignDialogue(TextAsset inkJSONAsset)
    {
        dialogueController.inkJSONAsset = inkJSONAsset;
    }

    public void ActivateDialogue(bool active)
    {
        if (active)
        {
            dialoguePanel.alpha = 1.0f;
        }
        else
        {
            dialoguePanel.alpha = 0.0f;
        }

        dialoguePanel.blocksRaycasts = active;
        dialoguePanel.interactable = active;

        stopControl = active;
    }

    public void InitQuest(Quest quest)
    {
        if(quest == null)
        {
            return;
        }

        currentQuest = quest;
        questTitle.text = quest.questTitle;
        questDesc.text = quest.questDesc;
        questProgressStr.text = quest.questProgressStr;
        UpdateQuestStatus(false);
    }

    public void UpdateAcceptQuest(bool accept)
    {
        acceptQuest = accept;
    }

    public void UpdateQuestProgressUI(string text)
    {
        questProgressStr.text = text;
    }

    public void UpdateQuestStatus(bool done)
    {
        if(done)
        {
            questProgressStr.color = Color.green;
            questStatus.text = doneStr;

            Instance.StartCoroutine(HideQuestPanel());
        }
        else
        {
            questProgressStr.color = Color.white;
            questStatus.text = inProgStr;
        }        
    }

    IEnumerator HideQuestPanel()
    {
        yield return new WaitForSeconds(5.0f);
        questPanel.alpha = 0.0f;
        questPanel.blocksRaycasts = false;
    }
}
