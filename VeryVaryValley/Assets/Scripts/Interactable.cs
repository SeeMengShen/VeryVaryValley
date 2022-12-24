using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool locked;
    public CanvasGroup lockUI;
    public Throwing player;
    public GameObject desk;
    private string untaggedStr = "Untagged";

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

            GameController.Instance.fpc.m_MouseLook.SetCursorLock(false);
        }
        else
        {
            gameObject.tag = untaggedStr;        
        }
    }

    public void Unlock()
    {
        ShowLockUI(false);
        locked = false;
        StartCoroutine(AnimateOpen());
    }

    public void ShowLockUI(bool show)
    {
        if(show)
        {
            lockUI.alpha = 1.0f;
            player.readyToThrow = false;
        }
        else
        {
            lockUI.alpha = 0.0f;

            GameController.Instance.fpc.m_MouseLook.SetCursorLock(true);
            player.readyToThrow = true;
        }

        lockUI.blocksRaycasts = show;
        lockUI.interactable = show;

        GameController.Instance.stopControl = show;
    }

    IEnumerator AnimateOpen()
    {
        for(int i = 0; i < 10; i++)
        {
            desk.transform.position += new Vector3(0.0f, 0.0f, i/100f);
            yield return null;
        }        
    }
}
