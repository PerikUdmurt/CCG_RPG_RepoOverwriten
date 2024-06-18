using UnityEngine;
using Zenject;

namespace CCG.UI
{
    public class HUD : MonoBehaviour
    {
        public VariableJoystick joystick;
        public MyButton firstButton;
        public MyButton secondButton;

        public class Factory: PlaceholderFactory<HUD> { }
    }
}
