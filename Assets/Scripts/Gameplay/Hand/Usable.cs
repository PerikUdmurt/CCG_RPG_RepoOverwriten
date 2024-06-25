using System;
using UnityEngine;

namespace CCG.Gameplay.Hand
{
    public class Usable : MonoBehaviour, IUsable
    {
        public bool isUsable { get; set; } = true;

        public event Action Used;

        public void Use()
        {
            Used?.Invoke();
        }
    }
}