using _Project.Code.Core.Items;
using _Project.Code.Core.Items.Inventory;
using UnityEngine;

namespace _Project.Code.Tutorial
{
    public class StorageTrashView : MonoBehaviour
    {
        [SerializeField] private ItemInventory _inventory;

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

        private void OnRemoved(Item obj)
        {
        }

        private void OnAdded(Item collectable)
        {
            Destroy(collectable.gameObject);
        }

    }
}