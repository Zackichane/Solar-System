using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    // Add the names of scenes where the music manager should NOT persist
    [SerializeField]
    private string[] excludedScenes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene changes to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is in the excluded list
        foreach (string excludedScene in excludedScenes)
        {
            if (scene.name == excludedScene)
            {
                Destroy(gameObject); // Destroy the music manager
                instance = null;     // Clear the static instance
                return;
            }
        }
    }
}
