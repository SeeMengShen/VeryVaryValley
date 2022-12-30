using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneUIManager : MonoBehaviour
{
    public CanvasGroup[] canvasSet;

    public Slider masterSlider;
    public Slider backgroundSlider;
    public Slider effectSlider;
    public Toggle muteToggle;

    public CanvasGroup keyUI;
    public CanvasGroup powerBar;
    public Sprite leftClick;
    public Sprite rightClick;

    private const string LEFT_CLICK_STR = "Hold & Release";
    private const string RIGHT_CLICK_STR = "Click";

    private Color colorHolder = new Color();

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ChangeCanvasSet(canvasSet);
        UIManager.Instance.SetAudioUI(masterSlider, backgroundSlider, effectSlider, muteToggle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowKeyUI()
    {
        powerBar.alpha = 0.0f;
        keyUI.alpha = 1.0f;

        if (ItemBar.Instance.GetHoldingItem().throwable)
        {
            keyUI.GetComponentInChildren<Image>().sprite = leftClick;
            keyUI.GetComponentInChildren<TextMeshProUGUI>().text = LEFT_CLICK_STR;
            powerBar.alpha = 1.0f;
        }
        else if(ItemBar.Instance.GetHoldingItem().usable)
        {
            keyUI.GetComponentInChildren<Image>().sprite = rightClick;
            keyUI.GetComponentInChildren<TextMeshProUGUI>().text = RIGHT_CLICK_STR;
        }
        else
        {
            keyUI.GetComponentInChildren<Image>().sprite = null;
            keyUI.alpha = 0.0f;
        }
    }

    public void ShowReady(bool ready)
    {
        colorHolder = Color.clear;

        if (ready)
        {
            colorHolder.g = 1.0f;
            colorHolder.a = 0.2f;
        }
        else
        {
            colorHolder.a = 0.5f;
        }

        powerBar.gameObject.GetComponent<Image>().color = colorHolder;
    }
}
