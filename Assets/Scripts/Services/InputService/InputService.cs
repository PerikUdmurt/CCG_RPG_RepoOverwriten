using System;
using UnityEngine;

namespace CCG.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }

        public abstract event Action FirstButtonClicked;
        public abstract event Action FirstButtonUnclicked;
        public abstract event Action SecondButtonClicked;
        public abstract event Action SecondButtonUnclicked;
    }
}
