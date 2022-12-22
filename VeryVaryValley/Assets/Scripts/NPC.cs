using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string words;
    public TextAsset dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk()
    {
        GameController.Instance.AssignDialogue(dialogue);
        GameController.Instance.dialogueController.ResetAndStart();
        GameController.Instance.ActivateDialogue(true);
        GameController.Instance.fpc.m_MouseLook.SetCursorLock(false);
    }
}
