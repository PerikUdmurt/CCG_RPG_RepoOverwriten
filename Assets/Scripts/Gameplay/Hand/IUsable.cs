using System;

namespace CCG.Gameplay.Hand
{
    public interface IUsable
    {
        void Use();
        public event Action Used;
        public bool isUsable { get; set; }
    }
}