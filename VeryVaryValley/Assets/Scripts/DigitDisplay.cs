using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] digits;
    [SerializeField]
    private Image[] characters;
    private string codeSequence;
    private const string passwordStr = "1234";

    public Interactable safeBox;

    // Start is called before the first frame update
    void Start()
    {
        codeSequence = string.Empty;
        for (int i = 0; i <= characters.Length - 1; i++)
        {
            characters[i].sprite = digits[8];
        }

        //PushTheButton.ButtonPressed += AddDigitToCodeSequence;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddDigitToCodeSequence(int digitEntered)
    {
        
        if (codeSequence.Length < 4)
        {
            if (digitEntered >= 0)
            {
                codeSequence += digitEntered.ToString();
                DisplayCodeSequence(digitEntered);
            }
        }

        if (digitEntered == -1)
        {
            ResetDisplay();
        }
        else if (digitEntered == -2)
        {
            if (codeSequence.Length > 0)
            {
                CheckResults();
            }
        }
    }

    private void DisplayCodeSequence(int digitJustEntered)
    {
        switch (codeSequence.Length)
        {
            case 1:
                characters[0].sprite = digits[8];
                characters[1].sprite = digits[8];
                characters[2].sprite = digits[8];
                characters[3].sprite = digits[digitJustEntered];
                break;
            case 2:
                characters[0].sprite = digits[8];
                characters[1].sprite = digits[8];
                characters[2].sprite = characters[3].sprite;
                characters[3].sprite = digits[digitJustEntered];
                break;
            case 3:
                characters[0].sprite = digits[8];
                characters[1].sprite = characters[2].sprite;
                characters[2].sprite = characters[3].sprite;
                characters[3].sprite = digits[digitJustEntered];
                break;
            case 4:
                characters[0].sprite = characters[1].sprite;
                characters[1].sprite = characters[2].sprite;
                characters[2].sprite = characters[3].sprite;
                characters[3].sprite = digits[digitJustEntered];
                break;
        }
        characters[4 - codeSequence.Length].color = Color.green;
    }

    private void CheckResults()
    {
        if (codeSequence == passwordStr)
        {
            safeBox.Unlock();
        }
        else
        {
            StartCoroutine(WrongPassword());
        }
    }

    private void ResetDisplay()
    {
        for (int i = 0; i <= characters.Length - 1; i++)
        {
            characters[i].sprite = digits[8];
            characters[i].color = Color.clear;
        }
        codeSequence = string.Empty;
    }

    private IEnumerator WrongPassword()
    {
        foreach(Image c in characters)
        {
            c.color = Color.red;
        }

        yield return new WaitForSeconds(1.0f);

        ResetDisplay();
    }
}
