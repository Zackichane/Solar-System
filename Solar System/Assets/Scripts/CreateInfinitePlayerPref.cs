using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateInfinitePlayerPref : MonoBehaviour
{
    public Toggle infiniteToggle;
    void Start()
    {
        infiniteToggle.isOn = false;

        PlayerPrefs.SetInt("nInfinite", 0);
        PlayerPrefs.SetInt("infinite", 0);
        PlayerPrefs.SetInt("nHabitable", 0);
    }
    void Update()
    {
        if (infiniteToggle.isOn)
        {
            PlayerPrefs.SetInt("nInfinite", 1);
            PlayerPrefs.SetInt("infinite", 1);
        }
        else
        {
            PlayerPrefs.SetInt("nInfinite", 0);
            PlayerPrefs.SetInt("infinite", 0);
        }
    }	
}
