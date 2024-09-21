using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set this as the instance and don't destroy it on load
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object alive across scenes
        }
        else
        {
            // If an instance already exists, destroy the duplicate
            Destroy(gameObject);
        }
    }
}
