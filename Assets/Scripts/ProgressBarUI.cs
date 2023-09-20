using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        _cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        barImage.fillAmount = 0f;
        
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}