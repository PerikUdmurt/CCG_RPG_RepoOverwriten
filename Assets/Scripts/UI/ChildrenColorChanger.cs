using CCG.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildrenColorChanger : MonoBehaviour
{
    public Color color;
    
    public void ChangeColor()
    {
        List<Image> renderers = GetComponentsInChildren<Image>().ToList<Image>();
        foreach (Image renderer in renderers)
        {
            renderer.color = color;
        }
    }
}