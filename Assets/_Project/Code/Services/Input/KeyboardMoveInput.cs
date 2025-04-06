using UnityEngine;

namespace _Project.Code.Services.Input
{
    public class KeyboardMoveInput : IMoveInput
    {
        public Vector2 Direction => 
            new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical")).normalized;
    }
}