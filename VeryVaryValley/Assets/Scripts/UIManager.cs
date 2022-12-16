using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static float waitSeconds = 3.0f;
    private static string levelToLoad;



    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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
