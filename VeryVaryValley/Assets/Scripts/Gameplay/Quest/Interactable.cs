using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool locked;
    public CanvasGroup lockUI;
    public GameObject desk;
    private const string UNTAGGED_STR = "Untagged";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if(locked)
        {
            ShowLockUI(true);
        }
    }

    public void Unlock()
    {
        //play open sound
       
        AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.openCabinetEffect);


        ShowLockUI(false);
        locked = false;
        gameObject.tag = UNTAGGED_STR;
        StartCoroutine(AnimateOpen());
    }

    public void ShowLockUI(bool show)
    {
        if(show)
        {
            lockUI.alpha = 1.0f;
        }
        else
        {
            lockUI.alpha = 0.0f;
        }

        lockUI.blocksRaycasts = show;
        lockUI.interactable = show;

        LevelController.Instance.StopControl(show);
    }

    // Imitate open animation for cabinet
    IEnumerator AnimateOpen()
    {
        for(int i = 0; i < 10; i++)
        {
            desk.transform.localPosition += new Vector3(0.0f, 0.0f, i/100f);
            yield return null;
        }        
    }
}
