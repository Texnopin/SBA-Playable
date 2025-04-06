using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Tutorial
{
    public class InfinitySignAnimation : MonoBehaviour
    {
        [SerializeField] private float _width = 90f; // Размер ∞ (чем больше, тем шире)
        [SerializeField] private float _duration = 0.3f;
        
        private Tweener _tweener;
        
        private void AnimateInfinityLoop()
        {
            int resolution = 50; // Количество точек на пути (чем больше, тем плавнее)
            Vector3[] path = new Vector3[resolution];

            for (int i = 0; i < resolution; i++)
            {
                float t = i * 2 * Mathf.PI / resolution;
                float x = _width * Mathf.Cos(t) / (1 + Mathf.Sin(t) * Mathf.Sin(t));
                float y = _width * Mathf.Cos(t) * Mathf.Sin(t) / (1 + Mathf.Sin(t) * Mathf.Sin(t));
                path[i] = new Vector3(x, y, 0) + transform.localPosition;
            }

            transform.position = path[0]; // Установить начальную позицию

            _tweener = transform.DOPath(path, _duration, PathType.CatmullRom)
                .SetOptions(true) // true = замкнуть путь
                .SetEase(Ease.Linear)
                .SetLoops(-1).Play();
        }

        private void OnEnable() => AnimateInfinityLoop();

        private void OnDisable()
        {
            _tweener.Kill();
            _tweener = null;
        }
    }
}