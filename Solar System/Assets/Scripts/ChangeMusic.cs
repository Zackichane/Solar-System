using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip newMusic;

    public void ChangeToNewMusic()
    {
        if (audioSource != null && newMusic != null)
        {
            audioSource.Stop();

            audioSource.clip = newMusic;

            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned!");
        }
    }
}
