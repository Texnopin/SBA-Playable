using _Project.Code.Core.Items;
using _Project.Code.Core.Items.Inventory;

namespace _Project.Code.Core.Buyables
{
    public class InventorViewDestroyer : BaseInventoryView
    {
        protected override void OnRemoved(Item item)
        {
        }

        protected override void OnAdded(Item item) => Destroy(item.gameObject);
    }
}