using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInfinitePlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("nInfinite", 1);
        PlayerPrefs.SetInt("infinite", 1);

    }
}
