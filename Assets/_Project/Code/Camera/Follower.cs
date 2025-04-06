using UnityEngine;

namespace _Project.Code.Camera
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;
            
        private void Update()
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            
            targetPosition.z = transform.position.z;
            
            transform.position = targetPosition;
        }
    }
}