using System;
using _Project.Code.Services.Input;
using UnityEngine;

namespace _Project.Code.Core.Character
{
    public class Mover : MonoBehaviour 
    {
        [SerializeField] public float _speed = 10f;
        [SerializeField] private Rigidbody2D _rigidbody;

        private const float SpeedScaleFactor = 0.1f;

        private IMoveInput _input;

        public Vector2 Direction => _input.Direction;

        private Vector2 previousPosition;

        public bool isMoving;

        private void Start()
        {
            previousPosition = _rigidbody.position;
        }

        public void Init(IMoveInput input)
        {
            _input = input;
        }


        private void FixedUpdate()
        {
            if (_input is null) _input = new KeyboardMoveInput();

            var targetPosition = _rigidbody.position + Direction * (_speed * SpeedScaleFactor * Time.fixedDeltaTime);
            
            _rigidbody.MovePosition(targetPosition);






            var targetPosition_2 = _rigidbody.position + Direction * (_speed * SpeedScaleFactor * Time.fixedDeltaTime);
            _rigidbody.MovePosition(targetPosition_2);

            isMoving = _rigidbody.position != previousPosition;
            previousPosition = _rigidbody.position;
        }
    }
}