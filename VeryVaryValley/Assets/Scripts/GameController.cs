using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public GameObject canvas;
    public static TextMeshProUGUI uiText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiText = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ShowThreeSecondText(string text)
    {
        instance.StartCoroutine(ShowText(text));
    }

    public static IEnumerator ShowText(string text)
    {
        Debug.Log(1);
        uiText.gameObject.SetActive(true);
        uiText.text = text;
        yield return new WaitForSeconds(3.0f);
        uiText.gameObject.SetActive(false);
    }
}
