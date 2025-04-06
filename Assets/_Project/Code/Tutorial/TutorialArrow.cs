using _Project.Code.Core.Animations;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Tutorial
{
    public class TutorialArrow
    {
        private readonly Transform _holder;
        private readonly Transform _body;
        private readonly Transform _point1;
        private readonly Transform _point2;
        private Tween _animation;

        private int _state = -1;

        public TutorialArrow(Transform holder, Transform body, Transform point1, Transform point2)
        {
            _point1 = point1;
            _point2 = point2;
            _body = body;
            _holder = holder;
        }

        public void EnterNextState()
        {
            _state++;

            switch (_state)
            {
                case 0:
                    _holder.gameObject.SetActive(true);
                    _holder.position = _point1.position;
                    
                    _animation?.Kill();
                    _animation = AnimationsFactory.CreatePointing(_body);
                    _animation.Play();
                    
                    break;
                case 1:
                    _holder.position = _point2.position;
                
                    _animation?.Kill();
                    _animation = AnimationsFactory.CreatePointing(_body);
                    _animation.Play();

                    break;
                case 2:
                    _holder.gameObject.SetActive(false);
                    
                    _animation.Kill();
                    _animation = null;
                    
                    _state = -1;
                    return;
            }

        }
    }
}