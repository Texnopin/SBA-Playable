using UnityEngine;

namespace _Project.Code.Services.Input
{
    public class MoveInput : IMoveInput
    {
        public bool Enabled = true;
        
        private readonly Joystick _joystick;
        private readonly KeyboardMoveInput _keyboardMoveInput = new KeyboardMoveInput();

        public MoveInput(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector2 Direction => GetDirection();

        public Vector2 GetDirection()
        {
            if (!Enabled) return Vector2.zero;
            
            var joystick = _joystick.Direction;
            var keyboard = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical")).normalized;
            
            if (joystick.magnitude > 0.1f) return joystick;
            
            if (keyboard.magnitude > 0.1f) return keyboard;
            
            return Vector2.zero;
        }
    }
}