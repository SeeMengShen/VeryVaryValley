using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Clock : MonoBehaviour
{
    private DateTime currentTime;
    private int currentMinute;
    private TextMeshProUGUI timeText;   

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        UpdateTimeStr();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMinute != System.DateTime.Now.Minute)
        {
            UpdateTimeStr();
        }
    }

    private void UpdateTimeStr()
    {
        currentTime = System.DateTime.Now;
        currentMinute = currentTime.Minute;
        timeText.text = currentTime.ToShortTimeString();
    }
}
