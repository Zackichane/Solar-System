using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabitableZoneToggle : MonoBehaviour
{
    public GameObject habitableZone;
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = true;
        toggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    void ToggleValueChanged(bool value)
    {
        habitableZone.SetActive(value);
        toggle.isOn = value;
    }
}
