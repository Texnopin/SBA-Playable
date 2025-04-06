using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Selling.Line
{
    public class CustomerAppearanceAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _bubble;

        private Vector3 _startBodyScale;
        private Vector3 _startBubbleScale;

        private void Awake()
        {
            _startBodyScale = _body.transform.localScale;
            _startBubbleScale = _bubble.transform.localScale;
        }

        private void OnEnable()
        {
            _bubble.gameObject.SetActive(false);
            _bubble.transform.localScale = Vector3.zero;
            var anim = _bubble.transform.DOScale(_startBubbleScale, 0.5f);
        
            _body.transform.localScale = Vector3.zero;
            _body.transform.DOScale(_startBodyScale, 0.5f).Play()
                .OnComplete(() =>
                {
                    _bubble.gameObject.SetActive(true);
                    anim.Play();
                });
        }
    }
}