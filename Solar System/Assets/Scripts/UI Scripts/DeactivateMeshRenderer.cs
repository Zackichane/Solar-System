using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateMeshRenderer : MonoBehaviour
{
    [Header("UI Button")]
    public Button deactivateButton; // The UI button to trigger the action

    [Header("Target Objects")]
    public List<GameObject> targetObjects; // List of objects with MeshRenderers to deactivate

    private void Start()
    {
        // Ensure the button is assigned and add the listener
        if (deactivateButton != null)
        {
            deactivateButton.onClick.AddListener(DeactivateMeshes);
        }
        else
        {
            Debug.LogError("Deactivate button is not assigned!");
        }
    }

    private void DeactivateMeshes()
    {
        // Loop through the list of objects and deactivate their MeshRenderers
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
                else
                {
                    Debug.LogWarning($"No MeshRenderer found on {obj.name}");
                }
            }
        }
    }
}
