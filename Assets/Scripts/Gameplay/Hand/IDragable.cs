using System;
using UnityEngine;

namespace CCG.Gameplay.Hand
{
    public interface IDragable
    {
        public bool isDragable { get; set; }
        public event Action Taken;
        public event Action Dropped;
        public void Drag(Vector3 targetPosition);
        public void Take();
        public void Drop();
    }
}
