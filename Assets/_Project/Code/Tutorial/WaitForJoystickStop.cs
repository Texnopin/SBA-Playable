using UnityEngine;

namespace _Project.Code.Tutorial
{
    public class WaitForJoystickStop : CustomYieldInstruction
    {
        private readonly Joystick _joystick;
        private readonly float _threshold;

        public WaitForJoystickStop(Joystick joystick, float threshold = 0.05f)
        {
            _joystick = joystick;
            _threshold = threshold;
        }

        public override bool keepWaiting => _joystick.Direction.magnitude >= _threshold;
    }
}