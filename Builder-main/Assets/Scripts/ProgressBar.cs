using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] public int maxValue;
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI percentText;

    private void Awake()
    {
        percentText.text = 0.ToString();
    }

    public void SetValue(float value)
    {
        var fillAmount = Mathf.Clamp01(value / maxValue);
        mask.fillAmount = fillAmount;
        percentText.text = ToShort(fillAmount * 100);
        if (!_tweener.IsActive())
        {
            _tweener = transform.DOScale(1.2f, 0.2f).SetUpdate(true).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo);
        }
    }

    public static string ToShort(float f)
    {
        return f switch
        {
            > 1000000000 => (f / 1000000000).ToString("0.0") + "B",
            > 1000000 => (f / 1000000).ToString("0.0") + "M", 
            > 1000 => (f / 1000).ToString("0.0") + "K",
            > 1 => f.ToString("0"),
            _ => f.ToString("0.0")
        };
    }

    private Tweener _tweener;
}
