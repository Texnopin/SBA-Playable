using UnityEngine;

namespace _Project.Code.Tutorial
{
    public class WaitForJoystickMove : CustomYieldInstruction
    {
        private readonly Joystick _joystick;
        private readonly float _threshold;

        public WaitForJoystickMove(Joystick joystick, float threshold = 0.05f)
        {
            _joystick = joystick;
            _threshold = threshold;
        }

        public override bool keepWaiting => _joystick.Direction.magnitude <= _threshold;
    }
}