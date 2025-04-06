using _Project.Code.Core.Items.Inventory;
using _Project.Code.Services.ItemsCreation;
using _Project.Code.Services.ServiceLocator;
using UnityEngine;

namespace _Project.Code.Core.Items
{
    public class InventoryInitializer : MonoBehaviour
    {
        [SerializeField] ItemInventory _inventory;
        [SerializeField] int _count;

        ItemsCreator _itemsCreator;

        private void Start()
        {
            _itemsCreator = L.Resolve<ItemsCreator>();
            
            var type = _inventory.ItemType;

            for (int i = 0; i < _count; i++)
            {
                var item = _itemsCreator.Create(type);
                item.enabled = false;
                _inventory.Add(item);
            }
        }
    }
}