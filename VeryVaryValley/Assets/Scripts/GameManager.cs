using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    //stack that store scene history so that can go back to previous scene
    public static Stack<string> stackHistory = new Stack<string>();

    //volume, max = 1, min = 0.0184
    public  float masterVolume= 1.0f;
    public  float backgroundVolume = 1.0f;
    public  float effectVolume= 1.0f;
    public  bool muteToggleIsOn= false; 

    //menu camera (background)
    //the camera that is used to capture the background of the menu and setting
    public Transform followingObject;


    private static bool gameHasInitialized = false;

    //USE THIS LOAD SCENE AND DONT USE SCENE MANAGER
    public static void LoadScene(string sceneName) {
        stackHistory.Push(GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        if (!gameHasInitialized) {
            string UIManager = "UIManager";
            string MenuCamera = "MenuCamera";


            //dont destory game manager and ui manager
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(GameObject.Find(UIManager));
            DontDestroyOnLoad(GameObject.Find(MenuCamera));

            //play background music
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            gameHasInitialized = true;
        }
    }

    private static UnityEngine.SceneManagement.Scene GetActiveScene() {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

}
