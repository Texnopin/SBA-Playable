using _Project.Code.Services.CoroutineRunner;
using _Project.Code.Services.Input;
using _Project.Code.Services.ServiceLocator;
using Assets.CourseGame.Develop.DI;
using UnityEngine;

namespace _Project.Code.Entry
{
    [RequireComponent(typeof(CoroutineRunner))]
    public class Entry : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        
        private void Awake()
        {
            L.Container = new DIContainer();

            L.Reg(_ => GetComponent<CoroutineRunner>());
            L.Reg(_ => new MoveInput(_joystick));
            
            L.Container.Initialize();
        }
        
        private void OnDestroy()
        {
            L.Container.Dispose();
            L.Container = null;
        }
    }
}