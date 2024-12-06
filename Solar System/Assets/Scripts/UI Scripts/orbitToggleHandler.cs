using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orbitToggleHandler : MonoBehaviour
{
    public Toggle toggle;
    private bool isActive = true;
    private GameObject[] orbits;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = true;
        toggle.onValueChanged.AddListener(ToggleValueChanged);
        // start a coroutine of 2 sec
        StartCoroutine(CheckForNewOrbits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckForNewOrbits()
    {
        yield return new WaitForSeconds(2f);
        orbits = GameObject.FindGameObjectsWithTag("Orbit");
    }

    void ToggleValueChanged(bool value)
    {
        Debug.Log("Toggle value changed to: " + value);
        isActive = value;
        // get the object with their name
        
        foreach (GameObject orbit in orbits)
        {
            orbit.SetActive(isActive);
        }
        toggle.isOn = value;
    }
}
