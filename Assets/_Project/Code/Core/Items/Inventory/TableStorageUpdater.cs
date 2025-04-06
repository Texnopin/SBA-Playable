using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Items.Inventory
{
    public class TableStorageUpdater : MonoBehaviour
    {
        [SerializeField] private ItemInventory _itemInventory;
        [SerializeField] private int _price = 10;

        [SerializeField] private GameObject _buyZone;
        [SerializeField] private GameObject _table;

        [SerializeField] private List<GameObject> _customers;

        private int _counter;
    
        private void OnEnable()
        {
            if (_itemInventory == null) return;
            _itemInventory.Added += OnAdded;
        }

        private void OnDisable()
        {
            if (_itemInventory == null) return;
            _itemInventory.Added -= OnAdded;
        }

        private void OnAdded(Item obj)
        {
            obj.transform.DOScale(Vector3.zero, 0.5f).Play()
                .OnComplete(() => obj.gameObject.SetActive(false));
        
            _counter++;

            if (_counter >= _price)
            {
                _itemInventory.SetOperation(InventoryType.None);
                _buyZone.SetActive(false);

                _table.transform.localScale = Vector3.zero; 
                _table.transform.DOScale(Vector3.one, 0.5f).Play();
                _table.SetActive(true);

                StartCoroutine(ShowCustomers());
            }
        }

        private IEnumerator ShowCustomers()
        {
            foreach (var customer in _customers)
            {
                customer.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}