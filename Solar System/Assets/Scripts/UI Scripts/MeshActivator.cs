using UnityEngine;
using UnityEngine.UI;

public class MeshActivator : MonoBehaviour
{
    // Array to hold the MeshRenderers you want to activate
    public MeshRenderer[] meshRenderersToActivate;

    // Reference to the UI Button
    public Button activateButton;

    void Start()
    {
        // Ensure the button is assigned and add a listener
        if (activateButton != null)
        {
            activateButton.onClick.AddListener(ActivateMeshRenderers);
        }
        else
        {
            Debug.LogError("No button assigned to activateButton!");
        }

        // Ensure all MeshRenderers are initially disabled
        foreach (MeshRenderer meshRenderer in meshRenderersToActivate)
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }

    // Method to activate the specified MeshRenderers
    void ActivateMeshRenderers()
    {
        foreach (MeshRenderer meshRenderer in meshRenderersToActivate)
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }
    }
}
