namespace _Project.Code.Tutorial
{
    public interface IBuyable
    {
        bool IsBought { get; }

        void Buy();
    }
}