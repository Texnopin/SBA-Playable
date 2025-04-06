using System;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Core.Character
{
    public class TopDownAnimator : MonoBehaviour
    {
        [SerializeField] TopDownAnimationConfig _config;
        [SerializeField] Animator _animator;
        [SerializeField] Transform _flipBody;

        DirectedAnimation _currentAnimation;

        public Vector2 Direction { get; set; }

        void Update()
        {
            var animation = GetClosestAnimation(Direction);

            if (animation is null || animation.Equals(_currentAnimation)) return;

            Flip(animation.Flip);

            _animator.Play(animation.Clip.name);

            _currentAnimation = animation;
        }

        private void OnDisable()
        {
            _animator.Rebind();
            _animator.Update(0);
            
            _animator.SetTrigger("Reset");
        }

        private void Flip(bool flip)
        {
            if (_flipBody == null) return;

            _flipBody.localScale = flip
                ? new Vector3(-1, 1, 1)
                : new Vector3(1, 1, 1);
        }

        DirectedAnimation GetClosestAnimation(Vector2 direction)
        {
            var closest = _config
                .Animations
                .OrderByDescending(x =>
                    Vector2.Dot(direction.normalized, x.Direction.normalized))
                .First();

            return closest;
        }
    }
}