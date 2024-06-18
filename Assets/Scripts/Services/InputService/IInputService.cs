using System;
using UnityEngine;

namespace CCG.Services.Input
{
    public interface IInputService
    {
        public Vector2 Axis { get; }
        public event Action FirstButtonClicked;
        public event Action FirstButtonUnclicked;
        public event Action SecondButtonClicked;
        public event Action SecondButtonUnclicked;
    }
}
