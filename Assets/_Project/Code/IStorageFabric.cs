using _Project.Code.Core.Items;
using UnityEngine;

namespace _Project.Code.Tutorial
{
    public interface IStorageFabric
    {
        IStorage CreateNew(Vector3 position, ItemType type, int count);
    }
}