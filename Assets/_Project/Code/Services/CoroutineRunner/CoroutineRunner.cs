using System.Collections;
using UnityEngine;

namespace _Project.Code.Services.CoroutineRunner
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner 
    {
        public void Launch(IEnumerator coroutine) => StartCoroutine(coroutine);
        public void Stop(IEnumerator coroutine) => StopCoroutine(coroutine);
    }
}