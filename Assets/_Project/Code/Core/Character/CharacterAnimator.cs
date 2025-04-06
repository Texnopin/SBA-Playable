using UnityEngine;

namespace _Project.Code.Core.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] TopDownAnimator _walkAnimator;
        [SerializeField] TopDownAnimator _idleAnimator;
        [SerializeField] TopDownAnimator _backpackAnimator;

        public Vector2 Direction {get; set;}

        private void Update()
        {
            var isMoving = Direction.magnitude > 0.1f;
            
            _walkAnimator.enabled = isMoving;
            _idleAnimator.enabled = !isMoving;

            //if (!isMoving) return;

            _walkAnimator.Direction = Direction;
            _idleAnimator.Direction = _walkAnimator.Direction;
            _backpackAnimator.Direction = _idleAnimator.Direction;
        }
    }
}