using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance = null;


    public AudioSource backgroundAudio;
    public AudioSource effectAudio;

    public AudioClip clickingEffect;

    void Awake() {
        if (Instance == null) {
            Instance = this;

        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

    }

    //play background music
    public void PlayBackgroundMusic() {
        Debug.Log(backgroundAudio.clip.name);
        backgroundAudio.Play();
    }

    public void PlayButtonClicking() {
        effectAudio.PlayOneShot(clickingEffect, 1);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            PlayButtonClicking();
        }
    }
}
