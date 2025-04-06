using System;
using _Project.Code.Core.Collision;
using _Project.Code.Core.Items.Inventory;
using _Project.Code.Core.Items.Transfering;
using UnityEngine;

namespace _Project.Code.Core.Items.Handlers
{
    public class ItemCollisionHandler : IDisposable
    {
        private readonly ICollisionDetector _detector;
        private readonly InventoryProvider _provider;
        private readonly ItemTransferer _transferer;

        public ItemCollisionHandler(ICollisionDetector detector, InventoryProvider provider, ItemTransferer transferer)
        {
            _transferer = transferer;
            _provider = provider;
            _detector = detector;
            _transferer = transferer;

            _detector.Triggered += OnTriggered;
        }

        public void Dispose() => _detector.Triggered -= OnTriggered;

        private void OnTriggered(Collider2D obj, CollisionType type)
        {
            if (type == CollisionType.Exit) return;

            if (!obj.TryGetComponent(out Item item) ||
                !item.enabled)
                return;
            
            if (_provider.TryGetStorage(item.Type, out var storage) == false)
                return;

            var result = _transferer.TryTransfer(item, storage);

            Debug.Log($"Transfering {item.Type} to {storage.ItemType}: " + (result ? "success" : "failed"));
        }
    }
}