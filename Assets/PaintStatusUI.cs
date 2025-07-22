using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintStatusUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text statusText;
    public Image statusImage;

    [Header("Colors")]
    public Color overpaintColor = Color.red;
    public Color correctColor = Color.green;
    public Color underpaintColor = Color.grey;
    public Color defaultColor = Color.white;
   // public P3DPaintedPercentage paintedPercentageScript;

    [SerializeField] private float percentageTxt;

    private void Start()
    {
        percentageTxt = P3DPaintedPercentage.instance.paintedPercentage * 100f;

        UpdateStatus(percentageTxt);
    }

    public void UpdateStatus(float percentage)
    {
        if (statusText == null || statusImage == null)
        {
            Debug.LogWarning("Status Text or Image is not assigned.");
            return;
        }

        if (percentage > 100f)
        {
            statusText.text = "Overpainted";
            statusImage.color = overpaintColor;
        }
        else if (percentage > 85f && percentage <= 100f)
        {
            statusText.text = "Correct";
            statusImage.color = correctColor;
        }
        else if (percentage < 75f)
        {
            statusText.text = "Underpaint";
            statusImage.color = underpaintColor;
        }
        else
        {
            statusText.text = "";
            statusImage.color = defaultColor;
        }
    }
}