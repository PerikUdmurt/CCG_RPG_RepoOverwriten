using System;
using UnityEngine;
using Zenject;

namespace CCG.Services.Input
{
    public class DesktopInputService : InputService, ITickable
    {
        public override Vector2 Axis => DesktopAxis();

        public override event Action FirstButtonClicked;
        public override event Action FirstButtonUnclicked;
        public override event Action SecondButtonClicked;
        public override event Action SecondButtonUnclicked;

        private Vector2 _playerPosition;
        private Camera _camera = Camera.main;

        public DesktopInputService(GameObject player)
        {
            _playerPosition = player.transform.position;
        }

        public void Tick()
        {
            
            if (UnityEngine.Input.GetButtonDown("FirstButtonClick"))
                { FirstButtonClicked?.Invoke(); }
            if (UnityEngine.Input.GetButtonDown("FirstButtonUnclick"))
                { FirstButtonUnclicked?.Invoke(); }
            if (UnityEngine.Input.GetButtonDown("SecondButtonClick"))
                { SecondButtonClicked?.Invoke(); }
            if (UnityEngine.Input.GetButtonDown("SecondButtonUnclick"))
                { SecondButtonUnclicked?.Invoke(); }
        
        }
      
        private Vector2 DesktopAxis()
        {
            Vector2 mousePoint = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vector2 result = _playerPosition + mousePoint;
            return result;
        }
    }
}
