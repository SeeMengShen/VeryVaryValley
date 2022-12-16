using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {

    //stack that store scene history so that can go back to previous scene
    public static Stack<string> stackHistory = new Stack<string>();

    //volume, max = 1, min = 0.0184, still got bug
    public static float masterVolume;
    public static float backgroundVolume;
    public static float effectVolume;

    private static bool gameHasInitialized = false;



    //the camera that is used to capture the background of the menu and setting


    //USE THIS LOAD SCENE AND DONT USE SCENE MANAGER
    public static void LoadScene(string sceneName) {
        stackHistory.Push(GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }

    void Awake() {
        if (!gameHasInitialized) {
            masterVolume = 1.0f;
            backgroundVolume = 1.0f;
            effectVolume = 1.0f;

            string findName = "UIManager";

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.Find(findName));
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            gameHasInitialized = true;
        }
    }

    private static UnityEngine.SceneManagement.Scene GetActiveScene() {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

}
