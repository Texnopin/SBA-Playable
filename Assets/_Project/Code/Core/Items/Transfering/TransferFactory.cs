using _Project.Code.Core.Items.Inventory;

namespace _Project.Code.Core.Items.Transfering
{
    public class TransferFactory
    {
        public bool TryCreate(Inventory.ItemInventory storage1, Inventory.ItemInventory storage2, out Transfer connection)
        {
            connection = null;
            
            if (storage1 == storage2) return false;

            if (!DoItemTypesMatch(storage1, storage2)) return false;

            if (CanGive(storage1) && CanGet(storage2))
            {
                connection = new Transfer(storage1, storage2);
                return true;
            }

            if (CanGive(storage2) && CanGet(storage1))
            {
                connection = new Transfer(storage2, storage1);
                return true;
            }

            return false;
        }

        private bool DoItemTypesMatch(Inventory.ItemInventory storage1, Inventory.ItemInventory storage2)
        {
            if (storage1.ItemType == ItemType.None || storage2.ItemType == ItemType.None)
            {
                return storage1.ItemType != storage2.ItemType;
            }

            return storage1.ItemType == storage2.ItemType;
        }

        private bool CanGet(Inventory.ItemInventory itemInventory) =>
            itemInventory.InventoryType == InventoryType.Receiver || itemInventory.InventoryType == InventoryType.Both;

        private bool CanGive(Inventory.ItemInventory itemInventory) =>
            itemInventory.InventoryType == InventoryType.Sender || itemInventory.InventoryType == InventoryType.Both;
    }
}
