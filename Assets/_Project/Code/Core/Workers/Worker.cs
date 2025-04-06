using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Code.Core.Collision;
using _Project.Code.Core.Harvesting;
using _Project.Code.Core.Items.Handlers;
using _Project.Code.Core.Items.Inventory;
using _Project.Code.Core.Items.Transfering;
using DG.Tweening;
using UnityEngine;

namespace _Project.Code.Core.Workers
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private OverlapCollisionDetector _detector;
        [SerializeField] private List<Field> _fields;
        [SerializeField] private ItemInventory _inventory;

        private bool _isUp;
        private bool _isMoving;

        public void Awake()
        {
            var handler = new ItemCollisionHandler(_detector, 
                new InventoryProvider(new List<ItemInventory> {_inventory}),
                new ItemTransferer());
            
            _fields.Sort((f1, f2) =>
                Vector3.Distance(transform.position, f1.transform.position)
                    .CompareTo(Vector3.Distance(transform.position, f2.transform.position)));
        }

        private void Update()
        {
            _animator.SetBool("IsMoving", _isMoving);
            //_animator.SetBool("IsUp", _isUp);
            
            transform.localScale = _isUp ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }

        public void OnEnable()
        {
            StartCoroutine(Working());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Working()
        {
            int index = 0;
            
            while (true)
            {
                yield return null;

                if (index >= _fields.Count) index = 0;

                var field = _fields[index];

                index++;

                if (!field.CanHarvest) continue;
                
                float distance = Vector3.Distance(transform.position, field.transform.position);

                var direction = field.transform.position - transform.position;

                _isUp = direction.y > 0;

                var randomDelay = UnityEngine.Random.Range(0.2f, 0.8f);
                
                yield return new WaitForSeconds(randomDelay);
                
                _isMoving = true;
                
                yield return transform.DOMove(field.transform.position, distance).SetEase(Ease.Linear).Play().WaitForCompletion();

                _isMoving = false;

                field.Harvest();
            }
        }
    }
}