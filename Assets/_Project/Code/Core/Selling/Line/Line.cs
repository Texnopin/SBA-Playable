using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Selling.Line
{
    public class Line : MonoBehaviour, ILine
    {
        [SerializeField] private Customer _customerPrefab;
        [SerializeField] private List<AnimatorController> _customerAnimatorControllers = new List<AnimatorController>();
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

        private Queue<Customer> _customers = new Queue<Customer>();

        private int _reservedCount;
        private int _spawnRequests;

        private void OnEnable() => Init();

        private int _lastGeneratedIndex = -1;

        private void Init()
        {
            RequestCreation(_spawnPoints.Count);

            StartCoroutine(CreatingCustomers());
        }

        private Customer CreateNewCustomer(int i)
        {
            var point = _spawnPoints[i];
            var customer = Instantiate(_customerPrefab, point, true);

            customer.transform.localPosition = Vector3.zero;

            int index;

            do
            {
                index = Random.Range(0, _customerAnimatorControllers.Count);
            } while (index == _lastGeneratedIndex);
            
            _lastGeneratedIndex = index;

            var animator = _customerAnimatorControllers[index];

            var orderSize = Random.Range(1, 10);

            customer.Init(animator, orderSize, orderSize);

            _customers.Enqueue(customer);

            return customer;
        }

        public bool TryGetCustomer(out Customer customer)
        {
            customer = _customers.Count > 0 ? _customers.Peek() : null;

            return customer != null;
        }

        public void OnCustomerServed(Customer customer)
        {
            var dequeued = _customers.Dequeue();

            if (customer != dequeued)
                throw new InvalidOperationException("Customer is not the same as dequeued");

            dequeued.enabled = false;

            MoveCustomers(RequestCreation);
        }

        private void RequestCreation(int count) => _spawnRequests += count;
        private void RequestCreation() => RequestCreation(1);

        private IEnumerator CreatingCustomers()
        {
            while (gameObject.activeSelf)
            {
                if (_spawnRequests < 1 && _customers.Count >= _spawnPoints.Count)
                {
                    yield return null;
                    continue;
                }

                var pointIndex = _customers.Count;
                var customer = CreateNewCustomer(pointIndex);

                _spawnRequests--;

                yield return new WaitForSeconds(2f);
            }


            //    customer.gameObject.SetActive(false);
            //    customer.enabled = false;

            //    customer.gameObject.SetActive(true);
            //    customer.enabled = true;
        }

        private void MoveCustomers(Action onFinished = null)
        {
            var customersArray = _customers.ToArray();
            _customers.Clear();

            var sequence = DOTween.Sequence();

            for (int i = 0; i < customersArray.Length + _reservedCount; i++)
            {
                var customer = customersArray[i];

                if (i < _spawnPoints.Count)
                {
                    customer.transform.parent = _spawnPoints[i];
                    sequence.Join(customer.transform.DOLocalMove(Vector3.zero, 0.5f));
                }

                _customers.Enqueue(customer);
            }

            sequence.OnComplete(() => onFinished?.Invoke()).Play();
        }
    }
}