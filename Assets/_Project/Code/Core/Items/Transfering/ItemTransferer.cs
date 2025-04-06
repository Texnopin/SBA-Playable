using System.Collections.Generic;
using _Project.Code.Core.Items.Inventory;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Items.Transfering
{
    public class ItemTransferer
    {
        private readonly Dictionary<ItemInventory, int> _reservations = new Dictionary<ItemInventory, int>();

        public bool TryTransfer(Item item, ItemInventory inventory)
        {
            if (CanTransfer(item, inventory))
            {
                Transfer(item, inventory);
                return true;
            }

            return false;
        }

        private void Transfer(Item item, ItemInventory to)
        {
            if (_reservations.TryGetValue(to, out var count))
                _reservations[to] += 1;
            else
                _reservations.Add(to, 1);

            item.transform.parent = null;
            item.enabled = false;

            item.transform.DOMove(to.transform.position, 0.2f).Play().OnComplete(() =>
            {
                to.Add(item);
                _reservations[to] -= 1;
                Debug.Log($"Transfered {item.Type} to {to}.");
            });
        }

        private bool CanTransfer(Item item, ItemInventory inventory) =>
            item.Type == inventory.ItemType &&
            inventory.CanAdd() &&
            GetReservedCount(inventory) + inventory.Count < inventory.Capacity;

        private int GetReservedCount(ItemInventory inventory) =>
            _reservations.ContainsKey(inventory) ? _reservations[inventory] : 0;
    }
}