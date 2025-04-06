using System.Collections.Generic;
using _Project.Code.Core.Items;
using _Project.Code.Core.Items.Inventory;
using _Project.Code.Tutorial;
using UnityEngine;

namespace _Project.Code.Core.Buyables
{
    public class Buyable : MonoBehaviour, IBuyable
    {
        [SerializeField] private ItemInventory _itemInventory;
        [SerializeField] private int _price = 10;

        [SerializeField] private GameObject _activateOnBought;
        [SerializeField] private List<GameObject> _deactivateOnBought;

        public bool IsBought => gameObject.activeSelf == false;

        private void Awake()
        {
            _itemInventory.SetType(ItemType.Coin);
            _itemInventory.SetOperation(InventoryType.Receiver);
            _itemInventory.SetCapacity(GetCapacity());
        }

        private void OnEnable()
        {
            _itemInventory.Added += OnAdded;
        }

        private void OnAdded(Item item)
        {
            if (_itemInventory.Count == _itemInventory.Capacity) Buy();
        }

        private void OnDisable()
        {
            _itemInventory.Added -= OnAdded;
        }

        private int GetCapacity() => _price;

        public void Buy()
        {
            _activateOnBought?.SetActive(true);
            _deactivateOnBought.ForEach(g => g.SetActive(false));
        }
    }
}