using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Code.Selling.Line
{
    public class BubbleView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _body;
        
        public void SetText(string text) => _text.text = text;

        private void OnEnable()
        {
            _body.localScale = Vector3.zero;
            _body.DOScale(Vector3.one, 1f).Play();
        }
    }
}