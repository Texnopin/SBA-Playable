using System;
using UnityEngine;

namespace _Project.Code.Core.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private ItemType _type;
        
        public ItemType Type => _type;
        
        private void OnEnable() => _collider.enabled = true;
        private void OnDisable() => _collider.enabled = false;
    }
}