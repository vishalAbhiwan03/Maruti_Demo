using System;
using UnityEngine;
using PaintIn3D;

public class P3DPaintedPercentage : MonoBehaviour
{
    public static P3DPaintedPercentage instance;
    public P3dPaintableTexture paintableTexture;
    public Color backgroundColor = Color.white;
    [Range(0f, 0.1f)] public float tolerance = 0.01f;

    [Range(0, 1)] public float paintedPercentage = 0f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void CalculatePainted()
    {
        if (paintableTexture == null)
        {
            Debug.LogError("🎨 No P3dPaintableTexture assigned.");
            return;
        }

        // Get the current painted texture as Texture2D
        Texture2D tex = paintableTexture.GetReadableCopy();

        if (tex == null)
        {
            Debug.LogError("🎨 Failed to get readable copy of painted texture.");
            return;
        }

        Color[] pixels = tex.GetPixels();
        int painted = 0;

        foreach (var pixel in pixels)
        {
            if (!IsSimilarColor(pixel, backgroundColor, tolerance))
            {
                painted++;
            }
        }

        paintedPercentage = (float)painted / pixels.Length*1.8f;

        Debug.Log($"🎨 Painted Percentage: {(paintedPercentage * 100f):F2}%");

        Destroy(tex); // clean up temporary copy
    }

    bool IsSimilarColor(Color a, Color b, float tol)
    {
        return Mathf.Abs(a.r - b.r) <= tol &&
               Mathf.Abs(a.g - b.g) <= tol &&
               Mathf.Abs(a.b - b.b) <= tol;
    }
}