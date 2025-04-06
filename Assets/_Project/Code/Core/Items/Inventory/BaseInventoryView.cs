using UnityEngine;

namespace _Project.Code.Core.Items.Inventory
{
    public abstract class BaseInventoryView : MonoBehaviour
    {
        [SerializeField] private ItemInventory _inventory;
        
        protected int ItemsCount => _inventory.Count;

        private void OnEnable()
        {
            if (_inventory == null) return;
        
            _inventory.Added += OnAdded;
            _inventory.Removed += OnRemoved;
        }

        private void OnDisable()
        {
            if (_inventory == null) return;
        
            _inventory.Added -= OnAdded;
            _inventory.Removed -= OnRemoved;
        }

        protected abstract void OnRemoved(Item item);

        protected abstract void OnAdded(Item item);
    }
}