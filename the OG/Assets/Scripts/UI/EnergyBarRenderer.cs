using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarRenderer : MonoBehaviour
{

    [SerializeField]
    private Image foreground;

    public void RenderHealth(float current, float max)
    {
        foreground.fillAmount = current / max;
    }
}
