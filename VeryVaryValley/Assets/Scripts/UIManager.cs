using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance = null;
    public CanvasGroup[] canvas;
    public float waitSeconds = 3.0f;
    private string levelToLoad;

    public CanvasGroup showingCanvas;
    private string[] canvasStr = { "Main Menu", "HowToPlay", "Settings" };

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

        Instance.showingCanvas = canvas[0];
    }

    IEnumerator Wait(float second)
    {
        yield return new WaitForSeconds(second);
        Application.Quit();
        Debug.Log("Quit");
    }

    public void PlayButtonPressed()
    {
        levelToLoad = "CutsceneScene";
        GameManager.LoadScene(levelToLoad);
        //Destroy(GameObject.Find("MenuCamera"));
    }

    public void QuitButtonPressed()
    {
        levelToLoad = "Quit";
        Instance.StartCoroutine(Wait(waitSeconds));
    }

    public void SettingsButtonPressed()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(2, true);
        return;

        /*levelToLoad = "SettingsScene";
        GameManager.LoadScene(levelToLoad);*/
    }

    public void HowToPlayButtonPressed()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(1, true);

        return;

        /*levelToLoad = "HowToPlayScene";
        GameManager.LoadScene(levelToLoad);*/
    }


    public void GoToPreviousScene()
    {
        Instance.HideCurrentCanvas();
        Instance.ShowCanvas(0, true);

        return;

        /*if (GameManager.stackHistory.Count > 0)
        {
            SceneManager.LoadScene(GameManager.stackHistory.Pop());

        }
        else
        {
            Debug.LogError("No previous scene!");
        }*/
    }

    public void ShowCanvas(int canvasIndex, bool show)
    {
        showingCanvas = canvas[canvasIndex];

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

}
