using _Project.Code.Core.Character;
using _Project.Code.Core.Items;
using _Project.Code.Core.Items.Inventory;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Selling
{
    public class Seller : MonoBehaviour
    {
        [SerializeField] private Item _moneyPrefab;
        [SerializeField] private Line.Line _lineMb;
        
        private ILine _line;

        [SerializeField] private ItemInventory _productInventory;
        [SerializeField] private ItemInventory _moneyInventory;

        private Customer _currentCustomer;
        
        private void Awake() => _line = _lineMb;

        private void Update()
        {
            if (_currentCustomer != null) return;

            if (!_line.TryGetCustomer(out var customer) || !customer.enabled) return;

            if (customer.OrderSize > _productInventory.Count) return;

            _currentCustomer = customer;

            Serve();
        }

        private void Serve()
        {
            var sequence = DOTween.Sequence();

            sequence.AppendInterval(0.5f);
            
            for (int i = 0; i < _currentCustomer.OrderSize; i++)
            {
                var item = _productInventory.Get();

                var tween = item.transform
                    .DOMove(_currentCustomer.transform.position, 0.5f)
                    .OnComplete(() => Destroy(item.gameObject));

                sequence.Join(tween);
            }

            sequence.OnComplete(GetPaid).Play();
        }

        private void GetPaid()
        {
            int count = _currentCustomer.Money + Random.Range(0, 6);

            var sequence = DOTween.Sequence();

            for (int i = 0; i < count; i++)
            {
                var money = Instantiate(_moneyPrefab);
                
                money.enabled = false;
                
                money.transform.position = _currentCustomer.transform.position;

                var tween = money.transform
                    .DOMove(_moneyInventory.transform.position, 0.5f)
                    .OnComplete(() => _moneyInventory.Add(money));

                sequence.Join(tween);
            }

            sequence.OnComplete(OnServed).Play();
        }

        private void OnServed()
        {
            var customer = _currentCustomer;
            _currentCustomer = null;
            
            _line.OnCustomerServed(customer);
        }
    }
}