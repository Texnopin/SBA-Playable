using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Animations
{
    public class AnimationsFactory
    {
        public static Tween CreatePointing(Transform body, float duration = 0.5f, float yOffset = 0.2f)
        {
            return body.DOMoveY(body.position.y - yOffset, duration).SetLoops(-1, LoopType.Yoyo);
        }

        public static Tween CreateInfinitySign(Transform body, float duration = 0.5f, float width = 50f) 
        {
            var transform = body;

            int resolution = 50; // Количество точек на пути (чем больше, тем плавнее)
            Vector3[] path = new Vector3[resolution];

            for (int i = 0; i < resolution; i++)
            {
                float t = i * 2 * Mathf.PI / resolution;
                float x = width * Mathf.Cos(t) / (1 + Mathf.Sin(t) * Mathf.Sin(t));
                float y = width * Mathf.Cos(t) * Mathf.Sin(t) / (1 + Mathf.Sin(t) * Mathf.Sin(t));
                path[i] = new Vector3(x, y, 0) + transform.position;
            }

            transform.position = path[0]; // Установить начальную позицию

            return transform.DOPath(path, duration, PathType.CatmullRom)
                .SetOptions(true) // true = замкнуть путь
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}