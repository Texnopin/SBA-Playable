using _Project.Code.Core.Items;
using _Project.Code.Core.Items.Inventory;
using TMPro;
using UnityEngine;

namespace _Project.Code.UI
{
    public class CoinsCounterUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ItemInventory _inventory;

        private int _counter = 0;

        private void OnEnable()
        {
            _inventory.Added += OnCoinAdded;
            _inventory.Removed += OnCoinRemoved;
        }

        private void OnDisable()
        {
            _inventory.Added -= OnCoinAdded;
            _inventory.Removed -= OnCoinRemoved;
        }

        private void OnCoinRemoved(Item obj)
        {
            _counter--;
            UpdateView();
        }

        private void OnCoinAdded(Item obj)
        {
            _counter++;
            UpdateView();
        }

        private void UpdateView() => _text.text = _counter.ToString();
    }
}