using System;
using UnityEngine;

namespace CCG.Gameplay.Hand
{
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
}

