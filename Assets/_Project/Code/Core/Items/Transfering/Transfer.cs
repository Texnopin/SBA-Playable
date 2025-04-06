namespace _Project.Code.Core.Items.Transfering
{
    public class Transfer
    {
        public Inventory.ItemInventory Sender { get; }
        public Inventory.ItemInventory Receiver { get; }

        public Transfer(Inventory.ItemInventory sender, Inventory.ItemInventory receiver)
        {
            Sender = sender;
            Receiver = receiver;
        }
    }
}
