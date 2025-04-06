using System;
using System.Collections.Generic;
using _Project.Code.Entry;
using _Project.Code.Tutorial;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Items.Inventory
{
    public class ItemInventory : MonoBehaviour, IStorage
    {
        public event Action<Item> Added;
        public event Action<Item> Removed;

        private Stack<Item> _collectables = new Stack<Item>();
        [SerializeField] private ItemType _type;
        [SerializeField] private InventoryType _inventory;
        [SerializeField] private int _capacity = 50;

        public bool CanAdd() => _collectables.Count < _capacity;
        public bool CanGet() => _collectables.Count > 0;

        public ItemType ItemType => _type;
        public InventoryType InventoryType => _inventory;
        public int Count => _collectables.Count;

        public bool IsEmpty => Count <= 0;

        public void Add(Item collectable)
        {
            Debug.Log($"Adding {collectable.Type} to {_type} storage");

            if (!CanAdd())
                throw new OperationCanceledException($"Cant add to {_type} storage");
                
            collectable.transform.localScale = Vector3.zero;

            collectable.transform.DOScale(Vector3.one, 0.1f).Play();

            _collectables.Push(collectable);

            Added?.Invoke(collectable);
        }

        public void SetCapacity(int capacity) => _capacity = capacity;
        public int Capacity => _capacity;

        public Item Get()
        {
            if (!CanGet())
                throw new InvalidOperationException("Storage is empty");

            var collectable = _collectables.Pop();
        
            Removed?.Invoke(collectable);

            return collectable;
        }

        public bool TryGet(out Item collectable)
        {
            if (!CanGet())
            {
                collectable = null;
                return false;
            }

            collectable = _collectables.Pop();
            return true;
        }

        public void SetType(ItemType type)
        {
            _type = type;
        }

        public void SetOperation(InventoryType type)
        {
            _inventory = type;
        }
    }
}