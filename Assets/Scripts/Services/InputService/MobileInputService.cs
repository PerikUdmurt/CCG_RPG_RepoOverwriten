using System;
using CCG.UI;
using UnityEngine;

namespace CCG.Services.Input
{
    public class MobileInputService : InputService, IInputService
    {
        public override Vector2 Axis => MobileAxis();

        public override event Action FirstButtonClicked;
        public override event Action FirstButtonUnclicked;
        public override event Action SecondButtonClicked;
        public override event Action SecondButtonUnclicked;

        private VariableJoystick _joystick;
        private MyButton _firstButton;
        private MyButton _secondButton;

        public void ConnectHUD(VariableJoystick variableJoystick, MyButton firstButton, MyButton secondButton)
        {
            _joystick = variableJoystick;
            _firstButton = firstButton;
            _secondButton = secondButton;
            _firstButton.Clicked += FirstButtonClick;
            _secondButton.Clicked += SecondButtonClick;
            _firstButton.Unclicked += FirstButtonUnclick;
            _secondButton.Unclicked += SecondButtonUnclick;
        }

        public void DiconnectHUD() 
        {
            _firstButton.Clicked -= FirstButtonClick;
            _secondButton.Clicked -= SecondButtonClick;
            _firstButton.Unclicked -= FirstButtonUnclick;
            _secondButton.Unclicked -= SecondButtonUnclick;
        }

        private void FirstButtonClick() { FirstButtonClicked?.Invoke(); }
        private void SecondButtonClick() { SecondButtonClicked?.Invoke(); }
        private void FirstButtonUnclick() { FirstButtonUnclicked?.Invoke(); }
        private void SecondButtonUnclick() { SecondButtonUnclicked?.Invoke(); }

        private Vector2 MobileAxis() {return _joystick.Direction.normalized;}
    }
}
