using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenuCustom : MonoBehaviour
{
    public Button button;
    public string SceneName;
    public int n;
    public string starName;
    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        PlayerPrefs.SetInt("starType", n);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("starName", starName);
        PlayerPrefs.Save();
        // load the scene
        SceneManager.LoadScene(SceneName);
    }
}
