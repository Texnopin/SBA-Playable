using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Core.Items;
using UnityEngine;

namespace _Project.Code.Services.ItemsCreation
{
    public class ItemsCreator
    {
        private List<Item> _prefabs;

        public ItemsCreator(List<Item> prefabs)
        {
            _prefabs = prefabs;
        }

        public Item Create(ItemType type)
        {
            var prefab = _prefabs.FirstOrDefault(p => p.Type == type);
            
            if (prefab is null)
                throw new NullReferenceException("No prefab for type " + type);
            
            return UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}