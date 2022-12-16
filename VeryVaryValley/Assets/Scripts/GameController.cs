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

    public static void ShowText(string text, bool delayForFiveSec)
    {
        if(text == string.Empty)
        {
            uiText.gameObject.SetActive(false);
            return;
        }

        if(delayForFiveSec)
        {
            instance.StartCoroutine(ShowFiveSecondText(text));
        }
        else
        {
            uiText.gameObject.SetActive(true);
            uiText.text = text;
        }
        
    }

    public static IEnumerator ShowFiveSecondText(string text)
    {
        uiText.gameObject.SetActive(true);
        uiText.text = text;
        yield return new WaitForSeconds(5.0f);
        uiText.gameObject.SetActive(false);
    }

    public static void ResetCollectable(GameObject collectable)
    {
        instance.StartCoroutine(ResetC(collectable));
    }

    private static IEnumerator ResetC(GameObject collectable)
    {
        collectable.GetComponent<Collectable>().availableQty = collectable.GetComponent<Collectable>().defaultQty;
        collectable.SetActive(false);
        yield return new WaitForSeconds(10f);
        collectable.SetActive(true);
    }
}
