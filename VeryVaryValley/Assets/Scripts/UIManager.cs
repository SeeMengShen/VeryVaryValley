using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager Instance = null;
    public float waitSeconds = 3.0f;
    private string levelToLoad;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    IEnumerator Wait(float second) {
        yield return new WaitForSeconds(second);
        Application.Quit();
        Debug.Log("Quit");
    }

    public void QuitButtonPressed() {
        levelToLoad = "Quit";
        StartCoroutine(Wait(waitSeconds));
    }

    public void SettingButtonPressed() {
        levelToLoad = "SettingScene";
        GameManager.LoadScene(levelToLoad);
    }

    public void GoToPreviousScene() {

        if (GameManager.stackHistory.Count > 0) {
            SceneManager.LoadScene(GameManager.stackHistory.Pop());

        }
        else {
            Debug.LogError("No previous scene!");
        }
    }



}
