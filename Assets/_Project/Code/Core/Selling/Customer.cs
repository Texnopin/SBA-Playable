using _Project.Code.Selling.Line;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;

namespace _Project.Code.Selling
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Customer : MonoBehaviour
    {
        [SerializeField] private BubbleView _bubbleView;
        
        private Animator _animator;
        
        public int OrderSize {get; private set;}
        public int Money {get; private set;}

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void Init(AnimatorController animatorController, int orderSize, int money)
        {
            OrderSize = orderSize;
            Money = money;
            
            _animator.runtimeAnimatorController = animatorController;
            _bubbleView.SetText(orderSize.ToString());
        }
        
        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f).Play()
                .OnComplete(() => _bubbleView.gameObject.SetActive(true));
        }
        
        private void OnDisable()
        {
            _bubbleView.gameObject.SetActive(false);
            transform.DOScale(Vector3.zero, 0.5f).Play();
        }
    }
}