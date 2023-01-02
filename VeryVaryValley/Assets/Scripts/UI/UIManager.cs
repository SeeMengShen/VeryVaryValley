using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    private CanvasGroup[] usingCanvasSet = new CanvasGroup[5];

    public Slider masterSlider;
    public Slider backgroundSlider;
    public Slider effectSlider;
    public Toggle muteToggle;

    public float waitSeconds = 3.0f;
    private string levelToLoad;
    private const string MASTER_STR = "Master";
    private const string BACKGROUND_STR = "Background";
    private const string EFFECT_STR = "Effect";
    

    public CanvasGroup showingCanvas;

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

    public void PlayButtonPressed()
    {        
        levelToLoad = GameManager.CUTSCENESCENE_STR;
        GameManager.LoadScene(levelToLoad);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void SettingsButtonPressed()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(2, true);
    }

    public void HowToPlayButtonPressed()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(1, true);
    }


    public void GoToPreviousScene()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(0, true);
    }

    public void ShowCanvas(int canvasIndex, bool show)
    {
        showingCanvas = usingCanvasSet[canvasIndex];

        if (show)
        {
            showingCanvas.alpha = 1.0f;
            showingCanvas.blocksRaycasts = true;
            showingCanvas.interactable = true;
        }
        else
        {
            showingCanvas.alpha = 0.0f;
            showingCanvas.blocksRaycasts = false;
            showingCanvas.interactable = false;
        }
    }

    public void HideCurrentCanvas()
    {
        showingCanvas.alpha = 0.0f;
        showingCanvas.blocksRaycasts = false;
        showingCanvas.interactable = false;
    }

    public void ChangeCanvasSet(CanvasGroup[] newCanvasSet)
    {
        usingCanvasSet = newCanvasSet;

        ShowCanvas(0, true);
    }

    public void Resume()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(0, true);
        LevelController.Instance.StopControl(false);
    }

    public void Pause()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(3, true);
        LevelController.Instance.StopControl(true);
    }

    public void SetAudioUI(Slider newMasterSlider, Slider newBackgroundSlider, Slider newEffectSlider, Toggle newMuteToggle)
    {
        masterSlider = newMasterSlider;
        backgroundSlider = newBackgroundSlider;
        effectSlider = newEffectSlider;
        muteToggle = newMuteToggle;

        AudioMixerManager.Instance.InitAudioUI();

        masterSlider.onValueChanged.AddListener(delegate { AudioMixerManager.Instance.SetValue(MASTER_STR); });
        backgroundSlider.onValueChanged.AddListener(delegate { AudioMixerManager.Instance.SetValue(BACKGROUND_STR); });
        effectSlider.onValueChanged.AddListener(delegate { AudioMixerManager.Instance.SetValue(EFFECT_STR); });
        muteToggle.onValueChanged.AddListener(delegate { AudioMixerManager.Instance.MuteMaster(); });
    }
}
