using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Core.Items.Inventory
{
    public class ColumnInventoryView : BaseInventoryView
    {
        [SerializeField] private List<Transform> _columnPoints;
        [SerializeField] private bool _destroyOnEmptied;

        private int _rowIndex = -1;
        private int _columnIndex = 0;

        private void Awake()
        {
            if (_columnPoints.Count < 1)
                throw new NullReferenceException("No column points");
        }

        protected override void OnRemoved(Item item)
        {
            _rowIndex--;

            if (_rowIndex < 0)
            {
                _rowIndex = _columnPoints.Count - 1;
                _columnIndex--;
            }

            item.transform.parent = null;
            
            if (_destroyOnEmptied && ItemsCount == 0) Destroy(gameObject);
        }

        protected override void OnAdded(Item item)
        {
            _rowIndex++;

            if (_rowIndex >= _columnPoints.Count)
            {
                _rowIndex = 0;
                _columnIndex++;
            }

            item.transform.parent = _columnPoints[_rowIndex];

            item.transform.localPosition = Vector3.zero + Vector3.up * Height;
            item.transform.rotation = Quaternion.identity;
        }

        private float Height => _columnIndex * 0.2f;
    }
}