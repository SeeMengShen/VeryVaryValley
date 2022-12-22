using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeypadButton : MonoBehaviour
{
    public DigitDisplay display;
    public int value;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        GetComponent<AudioSource>().Play();
        display.AddDigitToCodeSequence(value);
    }
}
