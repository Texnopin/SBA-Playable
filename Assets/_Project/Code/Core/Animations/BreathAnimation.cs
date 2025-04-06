using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Animations
{
    public class BreathAnimation : MonoBehaviour
    {
        [SerializeField] private float _scaleFactor = 1.2f;

        private Tweener _tweener;
        
        private void Animate() =>
            _tweener = transform
                .DOScale(Vector3.one * _scaleFactor, 0.5f)
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo)
                .Play();
        
        private void OnEnable() => Animate();
        
        private void OnDisable()
        {
            _tweener.Kill();
            _tweener = null;
        }
    }
}