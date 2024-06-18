using System;

namespace CCG.Gameplay.Hand
{
    public interface ISelectable
    {
        public void Select();
        public void Deselect();
        public event Action Selected;
        public event Action Deselected;
        public bool isSelectable { get; set; }
    }    
}