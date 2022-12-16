using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioMixerManager : MonoBehaviour {

    public const string MASTER_NAME = "Master";
    public const string BACKGROUND_NAME = "Background";
    public const string EFFECT_NAME = "Effect";

    

    public Slider masterSlider;
    public Slider backgroundSlider;
    public Slider effectSlider;
    public Toggle muteToggle;

    public AudioMixer audioMixer;

    private static bool firstTimeEnterSetting = true;

    void Start() {
        //got bug
        Debug.Log(GameManager.masterVolume);
        masterSlider.value = Mathf.Log(GameManager.masterVolume) * 20.0f;
        backgroundSlider.value = Mathf.Log(GameManager.backgroundVolume) * 20.0f;
        effectSlider.value = Mathf.Log(GameManager.effectVolume) * 20.0f;
    }

    public void SetValue(string audioGroup) {

        //if muted, no need to change
        if (muteToggle.isOn) return;

        //see which slider is changed and get the value
        float value;
        switch (audioGroup) {
            case MASTER_NAME: 
                value = masterSlider.value;
                GameManager.masterVolume = masterSlider.value; break;
            case BACKGROUND_NAME: 
                value = backgroundSlider.value;
                GameManager.backgroundVolume = backgroundSlider.value; break;
            case EFFECT_NAME:
                value = effectSlider.value;
                GameManager.effectVolume = effectSlider.value; break;
            default: return;
        }

       //change the audio mixer, log is to make the volume look nicer
        audioMixer.SetFloat(audioGroup, Mathf.Log(value) * 20.0f);
    }

    public void MuteMaster() {
        if (muteToggle.isOn) {
            audioMixer.SetFloat(MASTER_NAME, -80.0f);
            audioMixer.SetFloat(BACKGROUND_NAME, -80.0f);
            audioMixer.SetFloat(EFFECT_NAME, -80.0f);
        }
        else {
            audioMixer.SetFloat(MASTER_NAME, Mathf.Log(masterSlider.value) * 20.0f);
            audioMixer.SetFloat(BACKGROUND_NAME, Mathf.Log(backgroundSlider.value) * 20.0f);
            audioMixer.SetFloat(EFFECT_NAME, Mathf.Log(effectSlider.value) * 20.0f);
        }
    }

}

