using System.Collections;

namespace _Project.Code.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        void Launch(IEnumerator coroutine);
        void Stop(IEnumerator coroutine);
    }
}