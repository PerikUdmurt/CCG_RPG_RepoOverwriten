using CCG.Gameplay.Hand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    public bool isSelectable { get; set; } = true;

    public event Action Selected;
    public event Action Deselected;

    public void Deselect()
    {
        Deselected?.Invoke();
    }

    public void Select()
    {
        Selected?.Invoke();
    }
}
