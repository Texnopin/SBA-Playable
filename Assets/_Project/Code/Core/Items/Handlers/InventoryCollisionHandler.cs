using System;
using _Project.Code.Core.Collision;
using _Project.Code.Core.Items.Inventory;
using _Project.Code.Core.Items.Transfering;
using UnityEngine;

namespace _Project.Code.Core.Items.Handlers
{
    public class InventoryCollisionHandler : IDisposable
    {
        private readonly ICollisionDetector _detector;
        private readonly InventoryProvider _provider;
        private readonly InventoryTransferer _inventoryTransferer;

        public InventoryCollisionHandler(
            ICollisionDetector detector,
            InventoryProvider provider,
            InventoryTransferer inventoryTransferer)
        {
            _provider = provider;
            _detector = detector;
            _inventoryTransferer = inventoryTransferer;

            _detector.Triggered += OnTriggered;
        }

        public void Dispose() => _detector.Triggered -= OnTriggered;

        private void OnTriggered(Collider2D obj, CollisionType type)
        {
            if (!obj.TryGetComponent<ItemInventory>(out var inventory) ||
                !inventory.enabled)
                return;

            if (type == CollisionType.Exit && _inventoryTransferer.HasConnectionFor(inventory))
            {
                _inventoryTransferer.Disconnect();
                return;
            }

            if (_provider.TryGetStorage(inventory.ItemType, out var provided) == false)
                return;

            var result = _inventoryTransferer.TrySetConnection(provided, inventory);
                
            Debug.Log($"Connecting {inventory.ItemType} to {provided.ItemType}: " +
                      (result ? "Connected" : "Failed"));
        }
    }
}