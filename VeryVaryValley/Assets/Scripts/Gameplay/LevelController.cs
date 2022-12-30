using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityStandardAssets.Characters.FirstPerson;

public class LevelController : MonoBehaviour {
    public static LevelController Instance = null;

    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;

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

    public Quest currentSideQuest;
    public Quest currentMainQuest;

    public GameObject mainQuestIndicator;

    public Quest mainQuest1;
    public Quest mainQuest2;
    public CanvasGroup mainQuestPanel;
    private TextMeshProUGUI mainQuestTitle;
    private TextMeshProUGUI mainQuestDesc;
    private TextMeshProUGUI mainQuestProgressStr;
    private TextMeshProUGUI mainQuestStatus;

    public CanvasGroup sideQuestPanel;
    private TextMeshProUGUI sideQuestTitle;
    private TextMeshProUGUI sideQuestDesc;
    private TextMeshProUGUI sideQuestProgressStr;
    private TextMeshProUGUI sideQuestStatus;
    public bool acceptQuest = true;

    //cursor apper, stopControl == true
    private bool stopControl;
    public bool pause;

    private string doneStr = "Done";
    private string inProgStr = "In Progress";
    private string endCutsceneStr = "EndingCutsceneScene";

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        scaleHolder = new Vector3(1.0f, 0.0f, 1.0f);
        colorHolder = new Color(0.0f, 0.0f, 0.0f);

        AssignUI();

        InitMainQuest(mainQuest1);

        Physics.IgnoreLayerCollision(7, 8);

        CheckCurrentActiveCamera();
    }

    // Update is called once per frame
    void Update() {

        //Toggle Camera View
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CheckCurrentActiveCamera();
        }

        if (Input.GetKeyUp(KeyCode.M)) {
            showMap = !showMap;

            if (showMap) {
                map.alpha = 1.0f;
            }
            else {
                map.alpha = 0.0f;
            }

            map.blocksRaycasts = showMap;
            map.interactable = showMap;
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
            pause = !pause;

            if (pause) {
                UIManager.Instance.Pause();
            }
            else {
                UIManager.Instance.Resume();
            }
        }

        Instance.fpc.m_MouseLook.SetCursorLock(!stopControl);
    }

    private void CheckCurrentActiveCamera()
    {
        if (firstPersonCamera.activeInHierarchy)
        {
            thirdPersonCamera.SetActive(true);
            fpc.SetMainCamera(thirdPersonCamera.GetComponent<Camera>());
            firstPersonCamera.SetActive(false);
            PlayerAction.cam = thirdPersonCamera.transform;
        }
        else
        {
            firstPersonCamera.SetActive(true);
            fpc.SetMainCamera(firstPersonCamera.GetComponent<Camera>());
            thirdPersonCamera.SetActive(false);
            PlayerAction.cam = firstPersonCamera.transform;
        }
    }

    public void StopControl(bool stop) {
        Instance.StartCoroutine(DelayStopControl(stop));
    }

    private IEnumerator DelayStopControl(bool stop) {
        yield return new WaitForEndOfFrame();
        stopControl = stop;
    }

    public bool GetStopControl() {
        return stopControl;
    }

    private void AssignUI() {
        mainQuestTitle = mainQuestPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        mainQuestDesc = mainQuestPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        mainQuestStatus = mainQuestPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        mainQuestProgressStr = mainQuestPanel.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

        sideQuestTitle = sideQuestPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        sideQuestDesc = sideQuestPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        sideQuestStatus = sideQuestPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        sideQuestProgressStr = sideQuestPanel.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void ShowHintText(string text) {
        uiHintText.text = text;
    }

    public void ShowGrabText(string text) {
        grabText.text = text;
    }

    public void ShowWarningText(string text) {
        StartCoroutine(ShowFiveSecondText(text));
    }

    public IEnumerator ShowFiveSecondText(string text) {
        uiWarningText.text = text;
        yield return new WaitForSeconds(5.0f);
        uiWarningText.text = string.Empty;
    }

    public void ResetCollectable(GameObject collectable) {
        Instance.StartCoroutine(ResetC(collectable));
    }

    private IEnumerator ResetC(GameObject collectable) {
        collectable.GetComponent<Collectable>().availableQty = collectable.GetComponent<Collectable>().defaultQty;
        collectable.SetActive(false);
        yield return new WaitForSeconds(10f);
        collectable.SetActive(true);
    }

    public void PlayerCharging() {
        if (increase) {
            currentPower += chargingSpeed;

            if (currentPower >= maxPower) {
                increase = false;
                currentPower = maxPower;
            }
        }
        else {
            currentPower -= chargingSpeed;

            if (currentPower <= minPower) {
                increase = true;
                currentPower = minPower;
            }
        }

        UpdatePowerBar();
    }

    public void UpdatePowerBar() {
        scaleHolder.Set(1.0f, currentPower, 1.0f);
        colorHolder.r = 1.0f - currentPower;
        colorHolder.g = currentPower / 2.0f;

        power.rectTransform.localScale = scaleHolder;
        power.color = colorHolder;
    }

    public void ResetPowerBar() {
        currentPower = 0.0f;
        UpdatePowerBar();
    }

    public void AssignDialogue(TextAsset inkJSONAsset) {
        dialogueController.inkJSONAsset = inkJSONAsset;
    }

    public void ActivateDialogue(bool active) {
        if (active) {
            dialoguePanel.alpha = 1.0f;
        }
        else {
            dialoguePanel.alpha = 0.0f;
        }

        dialoguePanel.blocksRaycasts = active;
        dialoguePanel.interactable = active;

        stopControl = active;
    }

    public void InitSideQuest(Quest quest) {
        if (quest == null) {
            return;
        }

        currentSideQuest = quest;
        sideQuestTitle.text = quest.questTitle;
        sideQuestDesc.text = quest.questDesc;
        sideQuestProgressStr.text = quest.questProgressStr;
        UpdateSideQuestStatus(quest.done);
    }

    public void InitMainQuest(Quest quest) {
        currentMainQuest = quest;
        mainQuestTitle.text = quest.questTitle;
        mainQuestDesc.text = quest.questDesc;
        mainQuestProgressStr.text = quest.questProgressStr;
        UpdateMainQuestStatus(quest.done);
    }

    public void UpdateAcceptQuest(bool accept) {
        acceptQuest = accept;
    }

    public void UpdateSideQuestProgressUI(string text) {
        sideQuestProgressStr.text = text;
    }

    public void UpdateMainQuestProgressUI(string text) {
        mainQuestProgressStr.text = text;
    }

    public void UpdateSideQuestStatus(bool done) {
        if (done) {
            sideQuestProgressStr.color = Color.green;
            sideQuestStatus.text = doneStr;

            //play mission complete sound
            AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.questCompleteEffect);

            Instance.StartCoroutine(HideSideQuestPanel());
        }
        else {
            sideQuestProgressStr.color = Color.white;
            sideQuestStatus.text = inProgStr;
        }



    }

    public void UpdateMainQuestStatus(bool done) {
        if (currentMainQuest == mainQuest1) {
            if (done) {
                mainQuestProgressStr.color = Color.green;
                mainQuestStatus.text = doneStr;

                Instance.StartCoroutine(ChangeMainQuest());
            }
            else {
                mainQuestProgressStr.color = Color.white;
                mainQuestStatus.text = inProgStr;
            }
        }
        else if (currentMainQuest == mainQuest2) {
            if (done) {
                mainQuestProgressStr.color = Color.green;
                mainQuestStatus.text = doneStr;
                stopControl = true;
                Instance.StartCoroutine(PlayEndCutScene());

            }
            else {
                mainQuestProgressStr.color = Color.white;
                mainQuestStatus.text = inProgStr;
            }
        }
    }

    IEnumerator HideSideQuestPanel() {
        yield return new WaitForSeconds(5.0f);
        sideQuestPanel.alpha = 0.0f;
        sideQuestPanel.blocksRaycasts = false;
    }

    IEnumerator ChangeMainQuest() {
        yield return new WaitForSeconds(5.0f);
        InitMainQuest(mainQuest2);
        mainQuestIndicator.SetActive(true);
    }

    IEnumerator PlayEndCutScene() {
        yield return new WaitForSeconds(0.1f);
        GameManager.LoadScene(endCutsceneStr);
    }
}
