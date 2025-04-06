using UnityEngine;

namespace _Project.Code.Core.Items.Inventory
{
    public class StackInventoryView : MonoBehaviour
    {
        [SerializeField] private ItemInventory _itemInventory;
        [SerializeField] private float _spacing = 0.2f;
        [SerializeField] private Transform _attachTo;
        [SerializeField] private bool _destroyOnEmptied;
     
        private void OnEnable()
        {
            if (_itemInventory == null) return;
        
            _itemInventory.Added += OnAdded;
            _itemInventory.Removed += OnRemoved;
        }

        private void OnDisable()
        {
            if (_itemInventory == null) return;
        
            _itemInventory.Added -= OnAdded;
            _itemInventory.Removed -= OnRemoved;
        }

        private void OnRemoved(Item obj)
        {
            if (_itemInventory.Count == 0 && _destroyOnEmptied) Destroy(gameObject);
        }

        private void OnAdded(Item item)
        {
            item.transform.parent = _attachTo;
            item.transform.rotation = Quaternion.identity;

            var height = (_itemInventory.Count - 1) * _spacing;

            item.transform.localPosition = new Vector3(0, height, 0);

            Debug.Log($"Collected {item.Type}");
        }
    }
}