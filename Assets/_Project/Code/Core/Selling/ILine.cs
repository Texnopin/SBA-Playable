using _Project.Code.Core.Character;

namespace _Project.Code.Selling
{
    public interface ILine
    {
        bool TryGetCustomer(out Customer customer);
        void OnCustomerServed(Customer customer);
    }
}