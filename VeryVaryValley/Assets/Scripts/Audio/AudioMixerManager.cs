using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioMixerManager : MonoBehaviour {

    public static AudioMixerManager Instance = null;

    public const string MASTER_NAME = "Master";
    public const string BACKGROUND_NAME = "Background";
    public const string EFFECT_NAME = "Effect";
    public const string AUDIO_MIXER_PATH = "Sounds/AudioMixer";

    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider backgroundSlider;
    public Slider effectSlider;
    public Toggle muteToggle;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        audioMixer = (AudioMixer)Resources.Load(AUDIO_MIXER_PATH);
    }

    void Start() 
    {
        InitAudioUI();
    }

    public void InitAudioUI()
    {
        masterSlider = UIManager.Instance.masterSlider;
        backgroundSlider = UIManager.Instance.backgroundSlider;
        effectSlider = UIManager.Instance.effectSlider;
        muteToggle = UIManager.Instance.muteToggle;

        //always get value from game manager
        masterSlider.value = GameManager.Instance.masterVolume;
        backgroundSlider.value = GameManager.Instance.backgroundVolume;
        effectSlider.value = GameManager.Instance.effectVolume;
        muteToggle.isOn = GameManager.Instance.muteToggleIsOn;
    }

    public void SetValue(string audioGroup) 
    {
        //if muted, no need to change
        if (muteToggle.isOn) return;

        //see which slider is changed and get the value
        //also change the value in game manager so that the slider value can be stored
        float value;
        switch (audioGroup) {
            case MASTER_NAME:
                value = masterSlider.value;
                GameManager.Instance.masterVolume = masterSlider.value; break;
            case BACKGROUND_NAME:
                value = backgroundSlider.value;
                GameManager.Instance.backgroundVolume = backgroundSlider.value; break;
            case EFFECT_NAME:
                value = effectSlider.value;
                GameManager.Instance.effectVolume = effectSlider.value; break;
            default: return;
        }

        //change the audio mixer, log is to make the volume look nicer
        audioMixer.SetFloat(audioGroup, Mathf.Log(value) * 20.0f);
    }

    public void MuteMaster() {

        //if toggle is on (mute)
        if (muteToggle.isOn) {
            audioMixer.SetFloat(MASTER_NAME, -80.0f);
            audioMixer.SetFloat(BACKGROUND_NAME, -80.0f);
            audioMixer.SetFloat(EFFECT_NAME, -80.0f);
        }
        //if toggle is off (no mute)
        else {
            audioMixer.SetFloat(MASTER_NAME, Mathf.Log(masterSlider.value) * 20.0f);
            audioMixer.SetFloat(BACKGROUND_NAME, Mathf.Log(backgroundSlider.value) * 20.0f);
            audioMixer.SetFloat(EFFECT_NAME, Mathf.Log(effectSlider.value) * 20.0f);
        }

        //change the value in game manager
        GameManager.Instance.muteToggleIsOn = muteToggle.isOn;
    }
}

