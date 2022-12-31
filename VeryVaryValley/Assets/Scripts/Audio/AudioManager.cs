using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour {

    public const int mainMenuBGMID = 0;
    public const int inGameBGMID = 1;
    public const int endingBGMID = 2;

    public static AudioManager Instance = null;

    public AudioSource backgroundPlayer;
    public AudioSource effectPlayer;

    public AudioClip mainMenuBGM;
    public AudioClip inGameBGM;
    public AudioClip endingBGM;

    public AudioClip clickingEffect;

    public AudioClip gun1Effect;
    public AudioClip gun2Effect;
    public AudioClip gun3Effect;

    public AudioClip dropEffect;
    public AudioClip placeEffect;

    public AudioClip openCabinetEffect;

    public AudioClip pickUpEffect;
    public AudioClip throwingEffect;

    public AudioClip questCompleteEffect;

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
        backgroundPlayer.Play();
    }

    public void PlayButtonClicking() {
        effectPlayer.PlayOneShot(clickingEffect, 1);
    }

    public void PlayGunSound() {
        //randomize gun sound
        int randomValue = Random.Range(1, 3);

        if (randomValue == 1) {
            effectPlayer.PlayOneShot(gun1Effect, 1);
        }
        else if (randomValue == 2) { 
            effectPlayer.PlayOneShot(gun2Effect, 1);
        }
        else {
            effectPlayer.PlayOneShot(gun3Effect, 1);
        }
    }

    public void PlayOneShotSoundEffect(AudioClip audioClip) {
        effectPlayer.PlayOneShot(audioClip, 1);
    }

    //change background music
    public void ChangeBackgroundMusic(int bgmID) {
        switch (bgmID) {
            case mainMenuBGMID: backgroundPlayer.clip = mainMenuBGM; break;
            case inGameBGMID: backgroundPlayer.clip = inGameBGM; break;
            case endingBGMID: backgroundPlayer.clip = endingBGM; break;
        }
        backgroundPlayer.Play();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && Cursor.visible) {
            PlayButtonClicking();
        }
    }
}
