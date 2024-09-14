using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGenerator
{
    ColourSettings settings;
    Texture2D texture;
    const int textureResolution = 50;

    public void UpdateSettings(ColourSettings settings)
    {
        this.settings = settings;
        if (texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
        else if (texture.width != textureResolution)
        {
            texture.Resize(textureResolution, 1);
        }
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        if (settings != null && settings.planetMaterial != null)
        {
            settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
        }
    }

    public void UpdateColours()
    {
        if (settings == null)
        {
            Debug.LogError("Settings not initialized. Call UpdateSettings first.");
            return;
        }

        if (settings.gradient == null)
        {
            Debug.LogError("Gradient not set in ColourSettings.");
            return;
        }

        Color[] colours = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colours[i] = settings.gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colours);
        texture.Apply();

        if (settings.planetMaterial != null)
        {
            settings.planetMaterial.SetTexture("_texture", texture);
        }
        else
        {
            Debug.LogError("Planet material not set in ColourSettings.");
        }
    }
}
