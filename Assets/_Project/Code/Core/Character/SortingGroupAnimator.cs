using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Code.Core.Character
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortingGroupAnimator : MonoBehaviour
    {
        [SerializeField] private int _order;
       
        private SortingGroup _sortingGroup;

        private void OnValidate()
        {
            _sortingGroup = GetComponent<SortingGroup>();
            _sortingGroup.sortingOrder = _order;
        }

        private void Awake() => _sortingGroup = GetComponent<SortingGroup>();

        private void Update() => _sortingGroup.sortingOrder = _order;
    }
}