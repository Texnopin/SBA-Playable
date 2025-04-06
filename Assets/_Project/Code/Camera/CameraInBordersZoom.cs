using UnityEngine;

namespace _Project.Code.Camera
{
    public class CameraInBordersZoom : MonoBehaviour
    {
        [SerializeField] private Collider2D _borders;
        [SerializeField] private float _defaultCameraSize = 8;
        
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }
        
        private void Update()
        {
            Vector2 screenSize = GetScreenSizeInWorld();
            Vector2 boundsSize = _borders.bounds.size;

           var factor = CalculateFactor(screenSize, boundsSize);
           
           if (factor > 1)
            {
                _camera.orthographicSize /= factor;
                //_virtualCamera.m_Lens.OrthographicSize -= 0.1f;
            }
            
            var size = _camera.orthographicSize;
            
            if (size < _defaultCameraSize)
            {
                var nextSize = size + 0.5f;
                var multiplier = nextSize / size;
                
                if (CalculateFactor(screenSize * multiplier, boundsSize) > 1) return;
                
                _camera.orthographicSize = nextSize;
            }
        }

        public float CalculateFactor(Vector2 screenSize, Vector2 boundsSize) => screenSize.x / boundsSize.x;

        public Vector2 GetScreenSizeInWorld()
        {
            Vector3 topLeft = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height));
            Vector3 bottomRight = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

            return new Vector2(bottomRight.x - topLeft.x, topLeft.y - bottomRight.y);
        }
    }
}