using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Code.Core.Workers
{
    public class WorkersAnimation : MonoBehaviour
    {
        [SerializeField] private Transform[] _plants;
        [SerializeField] private Transform _worker;

        [FormerlySerializedAs("_offset")] [SerializeField]
        private float _durationOffset;

        [FormerlySerializedAs("_workerOffset")] [SerializeField]
        private Vector3 _endPointOffset;

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AnimateField());
                StartCoroutine(AnimateWorker());
                StartCoroutine(AnimateWorkerBreath());
            }
        }

        private IEnumerator AnimateWorkerBreath()
        {
            _worker.DOScale(Vector3.one * 1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo).Play().Goto(Random.Range(0f, 1f));
        
            yield return null;
        }

        private IEnumerator AnimateWorker()
        {
            var startPoint = _plants[0].position;
            var endPoint = _plants[^1].position + _endPointOffset;

            yield return new WaitForSeconds(0.5f + _durationOffset);

            _worker.DOMove(endPoint, _plants.Length).SetEase(Ease.Linear).Play();
        }

        private IEnumerator AnimateField()
        {
            yield return new WaitForSeconds(_durationOffset);

            for (int i = 0; i < _plants.Length; i++)
            {
                //var startPoint = _plants[i].position;
                //var endPoint = _plants[i + 1].position;

                //yield return _worker.DOMove(endPoint, 1f).Play().WaitForCompletion();

                yield return _plants[i].DOScale(Vector3.zero, 1f).Play().WaitForCompletion();
            }
        }
    }
}