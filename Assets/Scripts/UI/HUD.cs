using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private RectTransform _hintsEntryPos;

    public RectTransform HintEntryPos { get => _hintsEntryPos;}
}
