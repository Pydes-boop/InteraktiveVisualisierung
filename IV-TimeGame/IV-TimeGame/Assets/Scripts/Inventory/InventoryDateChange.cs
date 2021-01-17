//Script by Philipp
//changes the time text in the UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryDateChange : MonoBehaviour
{
    public Text dateText;
    public GameObject playerPrefab;

    public string prefix = "Date: ";
    public string suffix = "";

    private DateTime date0 = new DateTime(2020, 1, 1);
    private DateTime date1 = new DateTime(2030, 1, 1);
    private DateTime counter = new DateTime(2020, 1, 1);

    float diff;

    private void Awake()
    {
        diff = (float)(date1 - date0).TotalDays;
        setDateText();
        TimeSwapInput time = FindObjectOfType<TimeSwapInput>();
        if (time)
            time.OnSmoothToggle += timeSwap;
    }

    void Update()
    {
        
    }

    private void timeSwap(float state) 
    {
        if (state == 0)
        {
            counter = new DateTime(2020, 1, 1);
            setDateText();
            return;
        }
        else if(state == 1)
        {
            counter = new DateTime(2030, 1, 1);
            setDateText();
            return;
        }
        counter = date0.AddDays(Mathf.Lerp(0, diff, state));
        setDateText();
    }

    private void setDateText() 
    {
        dateText.text = prefix + counter.ToString("yyyy.MM.dd") + suffix;
    }
}
